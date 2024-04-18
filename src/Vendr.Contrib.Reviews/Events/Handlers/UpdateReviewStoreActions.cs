using System;
using System.Linq;
using Umbraco.Commerce.Cms.Web.Events.Notification;
using Umbraco.Commerce.Cms.Web.Models;
using Umbraco.Commerce.Common.Events;
using Umbraco.Commerce.Reviews.Models;
using Umbraco.Commerce.Reviews.Services;
using Umbraco.Commerce.Umbraco.Web.Events.Notification;
using Umbraco.Commerce.Umbraco.Web.Models;

namespace Umbraco.Commerce.Reviews.Events.Handlers
{
    public class UpdateReviewStoreActions : NotificationEventHandlerBase<StoreActionsRenderingNotification>
    {
        private readonly IReviewService _reviewService;

        public UpdateReviewStoreActions(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public override void Handle(StoreActionsRenderingNotification evt)
        {
            var statuses = new[] { ReviewStatus.Pending };
            var result = _reviewService.SearchReviews(evt.StoreId, statuses: statuses, startDate: DateTime.UtcNow.Date);

            if (result.TotalItems == 0)
                return;

            evt.Actions.Add(new StoreActionDto
            {
                Icon = Constants.Trees.Reviews.Icon,
                Description = $"<strong>{result.TotalItems + " " + (result.TotalItems == 1 ? "review" : "reviews")}</strong> {(result.TotalItems == 1 ? "is" : "are")} awaiting approval",
                RoutePath = $"#/commerce/vendrreviews/review-list/{evt.StoreId}?statuses={string.Join(",", statuses.Select(x => ((int)x).ToString() ))}"
            });
        }
    }
}