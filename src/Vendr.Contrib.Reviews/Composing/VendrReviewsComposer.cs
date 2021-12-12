using Vendr.Contrib.Reviews.Api;
using Vendr.Contrib.Reviews.Configuration;
using Vendr.Contrib.Reviews.Extensions;
using Vendr.Contrib.Reviews.Persistence;
using Vendr.Contrib.Reviews.Services.Implement;
using Vendr.Contrib.Reviews.Services;

#if NETFRAMEWORK
using Umbraco.Core;
using Umbraco.Core.Composing;
using IBuilder = Umbraco.Core.Composing.Composition;
#else
using Umbraco.Cms.Core.Composing;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;
#endif

namespace Vendr.Contrib.Reviews.Composing
{
    // ================================================================
    // IMPORTANT! Whatever you change here, be sure to also update the
    // v9 equivilent in /VendrReviewsUmbracoBuilderExtensions.cs
    // ================================================================

    public class VendrReviewsComposer : IUserComposer
    {
        public void Compose(IBuilder builder)
        {
            #if NETFRAMEWORK
                
            // Register settings
            builder.Register<VendrReviewsSettings>(Lifetime.Singleton);

            // Register API
            builder.Register<VendrReviewsApi>(Lifetime.Singleton);

            // Register factories
            builder.RegisterUnique<IReviewRepositoryFactory, ReviewRepositoryFactory>();

            // Register services
            builder.Register<IReviewService, ReviewService>(Lifetime.Singleton);

            // Register event handlers
            builder.AddVendrReviewsEventHandlers();

            // Register component
            builder.Components()
                .Append<VendrReviewsComponent>();
        #else
        // If Vendr Reviews hasn't been added manually by now, 
        // add it automatically with the default configuration.
        // If Vendr Reviews has already been added manully, then 
        // the AddVendrReviews() call will just exit early.
        builder.AddVendrReviews();
        #endif
        }
    }
}