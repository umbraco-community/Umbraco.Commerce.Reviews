#if NETFRAMEWORK
using HttpRequest = System.Web.HttpRequestBase;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
#endif

namespace Vendr.Contrib.Reviews.Web
{
    public static class RequestExtensions
    {
        public static string GetFormValue(this HttpRequest request, string key)
        {
#if NETFRAMEWORK
            return request.Form[key];
#else
            return request.HasFormContentType && request.Form[key] != StringValues.Empty ? request.Form[key].ToString() : "";
#endif
        }

        public static string GetMethod(this HttpRequest request)
        {
#if NETFRAMEWORK
            return request.HttpMethod;
#else
            return request.Method;
#endif
        }
    }
}
