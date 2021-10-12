#if NET

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Extensions;
using Vendr.Contrib.Reviewst.Services;

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
                .Bind(builder.Config.GetSection(VendrReviewsConstants.System.ProductName));

            if (defaultOptions != default)
                options.Configure(defaultOptions);

            options.ValidateDataAnnotations();

            // Register API
            builder.Register<VendrReviewsApi>(Lifetime.Singleton);

            // Register factories
            builder.RegisterUnique<IReviewRepositoryFactory, ReviewRepositoryFactory>();

            // Register event handlers
            builder.AddVendrEventHandlers();

            // Register services
            builder.Register<ReviewService>(Lifetime.Singleton);

            // Register component
            builder.Components()
                .Append<VendrReviewsComponent>();

            return builder;
        }
    }
}

#endif