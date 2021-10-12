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
        public static IBuilder AddVendrEventHandlers(this IBuilder builder)
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
