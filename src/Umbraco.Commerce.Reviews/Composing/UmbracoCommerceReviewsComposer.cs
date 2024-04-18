using Umbraco.Cms.Core.Composing;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;

namespace Umbraco.Commerce.Reviews.Composing
{
    public class UmbracoCommerceReviewsComposer : IComposer
    {
        public void Compose(IBuilder builder)
        {
            builder.AddUmbracoCommerceReviews();
        }
    }
}