using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevCommerce.WebUI.Controllers
{
    public class HomeController : ClientAuthController
    {
        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();

            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: "FiverSecurityScheme");

            return RedirectToAction("Index", "Login");
        }
    }
}