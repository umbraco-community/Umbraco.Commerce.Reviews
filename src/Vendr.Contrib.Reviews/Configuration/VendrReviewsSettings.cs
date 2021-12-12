#if NETFRAMEWORK
using System.Configuration;
#endif

namespace Vendr.Contrib.Reviews.Configuration
{
    public class VendrReviewsSettings
    {
        public HCaptcha HCaptcha { get; set; }

        public decimal MaxRating { get; } = 5;

        public VendrReviewsSettings()
        {
#if NETFRAMEWORK
            HCaptcha = new HCaptcha
            {
                SiteKey = ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SiteKey"]?.ToString(),
                SecretKey = ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SecretKey"]?.ToString()
            };
#endif
        }
    }

    public class HCaptcha
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }
    }
}
