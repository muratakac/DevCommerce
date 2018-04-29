using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    public class AccountController : ClientAuthController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}