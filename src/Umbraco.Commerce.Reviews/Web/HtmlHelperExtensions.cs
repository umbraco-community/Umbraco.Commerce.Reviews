using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Umbraco.Commerce.Reviews.Web
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderUmbracoCommerceReviews(this IHtmlHelper html, Guid storeId, string productReference)
            => RenderUmbracoCommerceReviews(html, "~/App_Plugins/UmbracoCommerceReviews/Views/Partials/VendrReviews.cshtml", storeId, productReference);

        public static IHtmlContent RenderUmbracoCommerceReviews(this IHtmlHelper html, string partialViewPath, Guid storeId, string productReference)
        {
            return html.Partial(partialViewPath, new ViewDataDictionary(html.ViewData)
            {
                { "storeId", storeId },
                { "productReference", productReference }
            });
        }
    }
}
