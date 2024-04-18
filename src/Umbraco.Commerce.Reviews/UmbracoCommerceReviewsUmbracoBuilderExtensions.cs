using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Vendr.Contrib.Reviews.Api;
using Vendr.Contrib.Reviews.Composing;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Extensions;
using Vendr.Contrib.Reviews.Notifications;
using Vendr.Contrib.Reviews.Persistence;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Services.Implement;

namespace Umbraco.Commerce.Reviews
{
    public static class UmbracoCommerceReviewsUmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddUmbracoCommerceReviews(this IUmbracoBuilder builder, Action<UmbracoCommerceReviewsSettings> defaultOptions = default)
        {
            // Register configuration
            var options = builder.Services.AddOptions<UmbracoCommerceReviewsSettings>()
                .Bind(builder.Config.GetSection("Umbraco:Commerce:Reviews"));

            options.ValidateDataAnnotations();

            options.ValidateDataAnnotations();

            // Register services
            builder.Services.AddTransient<IReviewRepositoryFactory, ReviewRepositoryFactory>();
            builder.Services.AddSingleton<IReviewService, ReviewService>();
            builder.Services.AddSingleton<VendrReviewsApi>();

            // Register event handlers
            builder.AddNotificationHandler<TreeNodesRenderingNotification, ReviewsTreeNodesNotification>();

            // Register event handlers
            builder.AddVendrReviewsEventHandlers();

            // Register component
            builder.Components()
                .Append<VendrReviewsComponent>();

            return builder;
        }
    }
}