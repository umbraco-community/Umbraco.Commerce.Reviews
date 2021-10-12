using System;

#if NETFRAMEWORK
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Umbraco.Core.Composing;
using IHtmlHelper = System.Web.Mvc.HtmlHelper;
using IHtmlContent = System.Web.IHtmlString;
#else
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
#endif

namespace Vendr.Contrib.Reviews.Web
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderVendrReviews(this IHtmlHelper html, Guid storeId, string productReference)
            => RenderVendrReviews(html, "~/App_Plugins/VendrReviews/Views/Partials/VendrReviews.cshtml", storeId, productReference);

        public static IHtmlContent RenderVendrReviews(this IHtmlHelper html, string partialViewPath, Guid storeId, string productReference)
        {
            return html.Partial(partialViewPath, new ViewDataDictionary
            {
                { "storeId", storeId },
                { "productReference", productReference }
            });
        }
    }
}