using System.Security.Claims;
using System.Security.Principal;

namespace DevCommerce.WebUI.Extensions
{
    public static class IdentityExtensions
    {
        public static string FullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string Organization(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Organization");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string Role(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Role");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string ProfileImage(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ProfileImage");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string Email(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Email");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
