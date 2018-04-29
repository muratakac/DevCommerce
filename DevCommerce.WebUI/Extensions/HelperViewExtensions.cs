using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DevCommerce.WebUI.Extensions
{
    public static class HelperViewExtensions
    {
        public static IHtmlContent NameOfCurrentUser(this IHtmlHelper helper)
        {
            var result = helper.ViewContext.HttpContext.User.Identity.FullName();
            return new HtmlString(result);
        }
    }
}
