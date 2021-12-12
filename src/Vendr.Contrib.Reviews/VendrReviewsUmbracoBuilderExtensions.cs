#if NET

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

namespace Vendr.Contrib.Reviews
{
    // ================================================================
    // IMPORTANT! Whatever you change here, be sure to also update the
    // v8 equivilent in /Composing/VendrReviewsComposer.cs
    // ================================================================

    public static class VendrReviewsUmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddVendrReviews(this IUmbracoBuilder builder, Action<VendrReviewsSettings> defaultOptions = default)
        {
            // Register configuration
            var options = builder.Services.AddOptions<VendrReviewsSettings>()
                .Bind(builder.Config.GetSection(Constants.System.ProductName));

            if (defaultOptions != default)
                options.Configure(defaultOptions);

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

#endif