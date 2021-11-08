#if NETFRAMEWORK
using Umbraco.Core.Composing;
using RazorPage = System.Web.Mvc.WebViewPage;
#else
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
#endif

namespace Vendr.Contrib.Reviews.Web
{
    public static class RazorPageExtensions
    {
        public static TService GetService<TService>(this RazorPage view)
        {
#if NETFRAMEWORK
            return (TService)Current.Factory.GetInstance(typeof(TService));
#else
            return (TService)view.Context.RequestServices.GetService(typeof(TService));
#endif
        }

        public static T GetSettings<T>(this RazorPage view)
            where T : class
        {
#if NETFRAMEWORK
            return view.GetService<T>();
#else
            return view.GetService<IOptions<T>>()?.Value;
#endif
        }

        public static bool IsUmbraco8(this RazorPage view)
        {
#if NETFRAMEWORK
            return true;
#else
            return false;
#endif
        }
    }
}