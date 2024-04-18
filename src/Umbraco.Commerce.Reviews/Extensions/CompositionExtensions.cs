using Umbraco.Commerce.Reviews.Events;
using Umbraco.Commerce.Reviews.Events.Handlers;
using Umbraco.Commerce.Extensions;
using Umbraco.Commerce.Cms.Web.Events.Notification;
using Umbraco.Commerce.Reviews.Notifications;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;

namespace Vendr.Contrib.Reviews.Extensions
{
    internal static class CompositionExtensions
    {
        public static IBuilder AddVendrReviewsEventHandlers(this IBuilder builder)
        {
            builder.WithNotificationEvent<ReviewAddedNotification>()
                .RegisterHandler<LogReviewAddedActivity>();

            builder.WithNotificationEvent<StoreActionsRenderingNotification>()
                .RegisterHandler<UpdateReviewStoreActions>();

            builder.WithNotificationEvent<ActivityLogEntriesRenderingNotification>()
                .RegisterHandler<UpdateReviewActivityLogBadge>();

            return builder;
        }
    }
}
