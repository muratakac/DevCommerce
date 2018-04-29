using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCommerce.WebUI.Utilities
{
    public class AuthenticationHelper
    {
        public static void CreateAuthCookie(Guid id, string userName, string email, DateTime expression, string[] roles,
            bool rememberMe, string firstName, string lastName)
        {
            //https://www.udemy.com/c-sharp-ile-kurumsal-solid-framework-gelistirme-4/learn/v4/t/lecture/7306956?start=0
            //var authTicket = new FormsAuthenticationTicket();
        }
    }
}
