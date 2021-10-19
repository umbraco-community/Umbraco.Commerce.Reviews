using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Events.Handlers;
using Vendr.Core.Events.Notification;
using Vendr.Extensions;
using Vendr.Umbraco.Web.Events.Notification;

#if NETFRAMEWORK
using IBuilder = Umbraco.Core.Composing.Composition;
#else
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;
#endif

namespace Vendr.Contrib.Reviews.Extensions
{
    internal static class CompositionExtensions
    {
        public static IBuilder AddVendrReviewsEventHandlers(this IBuilder builder)
        {
            #if NET
            builder.WithNotificationEvent<ReviewsTreeNodesNotification>()
                .RegisterHandler<TreeNodesRenderingNotification>();
            #endif

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
