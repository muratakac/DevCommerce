using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}