namespace Umbraco.Commerce.Reviews.Configuration
{
    public class UmbracoCommerceReviewsSettings
    {
        public HCaptcha HCaptcha { get; set; }

        public decimal MaxRating { get; } = 5;

        public UmbracoCommerceReviewsSettings()
        {
        }
    }

    public class HCaptcha
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }
    }
}
