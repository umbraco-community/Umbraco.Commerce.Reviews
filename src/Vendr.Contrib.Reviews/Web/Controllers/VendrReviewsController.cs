using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Vendr.Core;
using Vendr.Contrib.Reviews.Web.Dtos;
using Vendr.Core.Exceptions;
using Vendr.Core.Web.Api;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Models;
using System.Configuration;
using System.Net;
using System;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Vendr.Core.Models;
using Vendr.Core.Api;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Common.Models;
using Vendr.Common.Validation;

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    public class VendrReviewsController : SurfaceController, IRenderController
    {
        private readonly IVendrApi _vendrApi;
        private readonly IReviewService _reviewService;
        private readonly VendrReviewsSettings _settings;

        public VendrReviewsController(IVendrApi vendrAPi, IReviewService reviewService, VendrReviewsSettings settings)
        {
            _vendrApi = vendrAPi;
            _reviewService = reviewService;
            _settings = settings;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(AddReviewDto dto)
        {
            try
            {
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

            if (!string.IsNullOrWhiteSpace(_settings.HCaptchaSecretKey)
                && !string.IsNullOrWhiteSpace(_settings.HCaptchaSiteKey)
                && !string.IsNullOrWhiteSpace(hCaptchaResponse))
            {
                try
                {
                    var postData = $"response={hCaptchaResponse}&secret={_settings.HCaptchaSecretKey}&sitekey={_settings.HCaptchaSiteKey}";
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
                                _vendrApi.Log.Info<VendrReviewsController>("Failed hCaptcha validation with error codes: ",
                                    string.Join(", ", data["error-codes"].ToObject<string[]>()));

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
                    _vendrApi.Log.Error<VendrReviewsController>(ex, "Exception was thrown whilst validating a hCaptcha");
                }
            }
        }
    }
}