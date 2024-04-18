using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Umbraco.Commerce.Reviews.Web
{
    public static class RazorPageExtensions
    {
        public static TService GetService<TService>(this RazorPage view)
        {
            return (TService)view.Context.RequestServices.GetService(typeof(TService));
        }

        public static T GetSettings<T>(this RazorPage view)
            where T : class
        {
            return view.GetService<IOptions<T>>()?.Value;

        }
    }
}