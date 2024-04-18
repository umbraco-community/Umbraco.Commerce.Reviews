using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Umbraco.Commerce.Reviews.Web
{
    public static class RequestExtensions
    {
        public static string GetFormValue(this HttpRequest request, string key)
        {
            return request.HasFormContentType && request.Form[key] != StringValues.Empty ? request.Form[key].ToString() : "";
        }

        public static string GetMethod(this HttpRequest request)
        {
            return request.Method;
        }
    }
}
