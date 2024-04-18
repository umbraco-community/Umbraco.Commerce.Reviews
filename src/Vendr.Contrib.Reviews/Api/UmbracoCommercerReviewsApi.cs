using Umbraco.Commerce.Reviews.Services;

namespace Umbraco.Commerce.Reviews.Api
{
    public class UmbracoCommercerReviewsApi
    {
        public static UmbracoCommercerReviewsApi Instance { get; internal set; }

        private Lazy<IReviewService> _reviewService;

        public IReviewService ReviewService => _reviewService.Value;

        public UmbracoCommercerReviewsApi(Lazy<IReviewService> reviewService)
        {
            _reviewService = reviewService;
        }
    }
}