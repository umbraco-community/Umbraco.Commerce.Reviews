using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using Vendr.Common.Models;
using Vendr.Common.Validation;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Contrib.Reviews.Models;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Web.Dtos;
using Vendr.Core.Api;
using Vendr.Common.Logging;

#if NETFRAMEWORK
using IActionResult = System.Web.Mvc.ActionResult;
using Umbraco.Core;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;
#endif

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    public class VendrReviewsController : SurfaceController, IRenderController
    { 
        private readonly IVendrApi _vendrApi;
        private readonly IReviewService _reviewService;
        private readonly ILogger<VendrReviewsController> _logger;
        private readonly VendrReviewsSettings _settings;

#if NETFRAMEWORK
        public VendrReviewsController(IVendrApi vendrApi, IReviewService reviewService, ILogger<VendrReviewsController> logger, VendrReviewsSettings settings)
        {
            _vendrApi = vendrApi;
            _logger = logger;
            _reviewService = reviewService;
            _settings = settings;
        }
#else
        public VendrReviewsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, 
            ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
            IVendrApi vendrApi, IReviewService reviewService, ILogger<VendrReviewsController> logger, IOptions<VendrReviewsSettings> settings)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)

        {
            _vendrApi = vendrApi;
            _logger = logger;
            _reviewService = reviewService;
            _settings = settings.Value;
        }
#endif

        [HttpPost]
        [ValidateAntiForgeryToken]
#if NET
        [ValidateUmbracoFormRouteString]
#endif
        public IActionResult AddReview(AddReviewDto dto)
        {
            try
            {
                if (dto.Rating <= 0)
                {
                    ModelState.AddModelError("", "Rating for the review is required");
                    TempData["ErrorMessage"] = "Please select a rating";
                }

                if (!ModelState.IsValid)
                    return CurrentUmbracoPage();

                ValidateCaptcha();

                using (var uow = _vendrApi.Uow.Create())
                {
                    var review = new Review(dto.StoreId, dto.ProductReference, dto.CustomerReference)
                    {
                        Rating = dto.Rating,
                        Title = dto.Title,
                        Email = dto.Email,
                        Name = dto.Name,
                        Body = dto.Body,
                        RecommendProduct = dto.RecommendProduct
                    };

                    _reviewService.SaveReview(review);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to submit review: " + ex.Message);

                return CurrentUmbracoPage();
            }

            TempData["SuccessMessage"] = "Review successfully submitted";

            return RedirectToCurrentUmbracoPage();
        }

        private void ValidateCaptcha()
        {
            string hCaptchaResponse = Request.Form["h-captcha-response"];

            if (!string.IsNullOrWhiteSpace(_settings.HCaptcha?.SecretKey)
                && !string.IsNullOrWhiteSpace(_settings.HCaptcha?.SiteKey)
                && !string.IsNullOrWhiteSpace(hCaptchaResponse))
            {
                try
                {
                    var postData = $"response={hCaptchaResponse}&secret={_settings.HCaptcha.SecretKey}&sitekey={_settings.HCaptcha.SiteKey}";
                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var request = (HttpWebRequest)WebRequest.Create("https://hcaptcha.com/siteverify");
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Accept = "application/json";
                    request.Method = "POST";
                    request.ContentLength = byteArray.Length;

                    var dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    var response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusDescription == "OK")
                    {
                        var responseStream = response.GetResponseStream();

                        using (var reader = new StreamReader(responseStream))
                        {
                            var responseFromServer = reader.ReadToEnd();
                            var data = JObject.Parse(responseFromServer);

                            if (data["success"].Value<bool>() == false)
                            {
                                string[] errorCodes = data["error-codes"].ToObject<string[]>();

                                _logger.Info("Failed hCaptcha validation with error codes: ", string.Join(", ", errorCodes));

                                throw new ValidationException(new[]
                                {
                                    new ValidationError("Failed hCaptcha validation")
                                });
                            }
                        }
                    }
                }
                catch (ValidationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Exception was thrown whilst validating a hCaptcha");
                }
            }
        }
    }
}