using System.Linq;
using Vendr.Common.Events;
using Vendr.Core.Events.Notification;
using Vendr.Umbraco.Web.Events.Notification;

#if NET
using Microsoft.Extensions.Options;
#endif

namespace Vendr.Contrib.Reviews.Events.Handlers
{
    public class UpdateReviewActivityLogBadge : NotificationEventHandlerBase<ActivityLogEntriesRenderingNotification>
    {
        public override void Handle(ActivityLogEntriesRenderingNotification evt)
        {
            foreach (var entry in evt.LogEntries.Where(x => 
                x.StoreId == evt.StoreId && 
                x.EntityType == Constants.Entities.EntityTypes.Review))
            {
                entry.BadgeLabel = "Review";
                entry.BadgeColorClass = "vendr-bg--orange";
                entry.RoutePath = $"#/commerce/vendrreviews/review-edit/{evt.StoreId}_{entry.EntityId}";
            }
        }
    }
}