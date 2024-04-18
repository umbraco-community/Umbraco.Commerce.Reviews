﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Commerce.Core.Adapters;
using Umbraco.Commerce.Reviews.Helpers;
using Umbraco.Commerce.Reviews.Models;
using Umbraco.Commerce.Reviews.Persistence.Dtos;
using Umbraco.Commerce.Reviews.Services;
using Notification = Umbraco.Cms.Core.Models.ContentEditing.BackOfficeNotification;

namespace Umbraco.Commerce.Reviews.Web.Controllers
{
    [PluginController(Constants.Internals.PluginControllerName)]
    public class ReviewApiController : UmbracoAuthorizedApiController
    {
        private readonly IReviewService _reviewService;
        private readonly ILocalizedTextService _textService;
        private readonly IProductAdapter _productAdapter;

        public ReviewApiController(
            IReviewService reviewService,
            ILocalizedTextService textService,
            IProductAdapter productAdapter)
        {
            _reviewService = reviewService;
            _textService = textService;
            _productAdapter = productAdapter;
        }

        [HttpGet]
        public IEnumerable<ReviewStatusDto> GetReviewStatuses()
        {
            var values = Enum.GetValues(typeof(ReviewStatus));

            var statuses = new List<ReviewStatusDto>();
            int sortOrder = 1;

            foreach (ReviewStatus val in values)
            {
                var name = val.ToString();
                var color = ReviewHelper.GetStatusColor(val);

                statuses.Add(new ReviewStatusDto
                {
                    Alias = name.ToLower(),
                    Color = color,
                    Id = (int)val,
                    Name = name,
                    SortOrder = sortOrder
                });

                sortOrder++;
            }

            return statuses;
        }

        [HttpGet]
        public Dictionary<string, string> GetProductData(string productReference, string? languageIsoCode = null)
        {
            if (string.IsNullOrEmpty(languageIsoCode))
                languageIsoCode = Thread.CurrentThread.CurrentUICulture.Name;

            var snapshot = _productAdapter.GetProductSnapshot(productReference, languageIsoCode);
            if (snapshot == null)
                return null;

            return new Dictionary<string, string>
            {
                { "storeId", snapshot.StoreId.ToString() },
                { "sku", snapshot.Sku },
                { "name", snapshot.Name }
            };
        }

        

        [HttpGet]
        public ActionResult<ReviewEditDto> GetReview(Guid id)
        {
            var entity = _reviewService.GetReview(id);
            if (entity == null)
            {
                return NotFound();
            }

            return EntityMapper.ReviewEntityToEditDto(entity);
        }

        [HttpGet]
        public IEnumerable<ReviewDto> GetReviews(Guid[] ids)
        {
            return _reviewService.GetReviews(ids)
                .Select(x => EntityMapper.ReviewEntityToDto(x));
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForProduct(Guid storeId, string productReference, long pageNumber = 1, int pageSize = 30)
        {
            var result = _reviewService.GetReviewsForProduct(storeId, productReference, pageNumber, pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForCustomer(Guid storeId, string customerReference, long pageNumber = 1, int pageSize = 30)
        {
            var result = _reviewService.GetReviewsForCustomer(storeId, customerReference, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> SearchReviews(Guid storeId, [FromQuery] ReviewStatus[] statuses = null, [FromQuery] decimal[] ratings = null, string searchTerm = null, long pageNumber = 1, int pageSize = 30)
        {
            var result = _reviewService.SearchReviews(storeId, statuses: statuses, ratings: ratings, searchTerm: searchTerm, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpPost]
        public ReviewEditDto SaveReview(ReviewSaveDto review)
        {
            Review entity;

            try
            {
                entity = review.Id != Guid.Empty
                    ? _reviewService.GetReview(review.Id)
                    : new Review(review.StoreId, review.ProductReference, review.CustomerReference);

                EntityMapper.ReviewSaveDtoToEntity(review, entity);

                entity = _reviewService.SaveReview(entity);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("Failed saving review",  ex);
            }

            var model = EntityMapper.ReviewEntityToEditDto(entity);

            model.Notifications.Add(new Notification(
                _textService.Localize("speechBubbles", "operationSavedHeader", Thread.CurrentThread.CurrentUICulture),
                string.Empty, NotificationStyle.Success)
            );

            return model;
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteReview(Guid id)
        {
            _reviewService.DeleteReview(id);
        }

        [HttpPost]
        public ReviewEditDto ChangeReviewStatus(ChangeReviewStatusDto model)
        {
            var entity = _reviewService.ChangeReviewStatus(model.ReviewId, model.Status);

            return EntityMapper.ReviewEntityToEditDto(entity);
        }

        [HttpPost]
        public CommentDto SaveComment(CommentDto comment)
        {
            var entity = comment.Id.HasValue && comment.Id != Guid.Empty
                ? _reviewService.GetReview(comment.ReviewId).Comments.First(x => x.Id == comment.Id)
                : new Comment(comment.StoreId, comment.ReviewId);

            entity = EntityMapper.CommentDtoToEntity(comment, entity);

            _reviewService.SaveComment(entity);

            return EntityMapper.CommentEntityToDto(entity);
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteComment(Guid id)
        {
            _reviewService.DeleteComment(id);
        }
    }
}
