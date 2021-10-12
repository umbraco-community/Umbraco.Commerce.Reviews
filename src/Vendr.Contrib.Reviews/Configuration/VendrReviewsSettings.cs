#if NETFRAMEWORK
using System.Configuration;
#endif

namespace Vendr.Contrib.Reviews.Configuration
{
    public class VendrReviewsSettings
    {
        public string HCaptchaSiteKey { get; set; }

        public string HCaptchaSecretKey { get; set; }

        public VendrReviewsSettings()
        {
#if NETFRAMEWORK

            HCaptchaSiteKey = ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SiteKey"]?.ToString();
            HCaptchaSecretKey = ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SecretKey"]?.ToString();
#endif
        }
    }
}
