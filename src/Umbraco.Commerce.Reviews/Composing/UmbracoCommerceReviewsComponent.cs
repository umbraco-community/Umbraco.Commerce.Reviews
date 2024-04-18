using Umbraco.Commerce.Reviews.Api;
using Umbraco.Cms.Core.Composing;

namespace Umbraco.Commerce.Reviews.Composing
{
    public class UmbracoCommerceReviewsComponent : IComponent
    {
        private readonly UmbracoCommercerReviewsApi _api;

        public UmbracoCommerceReviewsComponent(UmbracoCommercerReviewsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            UmbracoCommercerReviewsApi.Instance = _api;
        }

        public void Terminate()
        {
        }
    }
}