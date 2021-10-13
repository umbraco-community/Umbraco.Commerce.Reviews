#if NET

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Vendr.Contrib.Reviews.Api;
using Vendr.Contrib.Reviews.Composing;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Extensions;
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

            // Register event handlers
            builder.AddVendrEventHandlers();

            // Register services
            builder.Services.AddSingleton<ReviewRepositoryFactory>();
            builder.Services.AddSingleton<VendrReviewsApi>();
            builder.Services.AddSingleton<ReviewService>();

            // Register component
            builder.Components()
                .Append<VendrReviewsComponent>();

            return builder;
        }
    }
}

#endif