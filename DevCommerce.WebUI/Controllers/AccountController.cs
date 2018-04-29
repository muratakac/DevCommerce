using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    [Authorize]
    public class AccountController : ClientAuthController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }
    }
}