using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevCommerce.Entities.Enums;
using DevCommerce.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevCommerce.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (User.Identity.IsAuthenticated)
                return View();


            string stringData = ClientBaseController.ServiceGetData("/api/Account/Login", RequestType.POST, JsonConvert.SerializeObject(loginViewModel), true);
            var result = JsonConvert.DeserializeObject(stringData);
            if (result.ToString() == "Giriş Başarılı")
            {
                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Sean Connery"),
                new Claim(ClaimTypes.Email, loginViewModel.Email)
            };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                var sign =  HttpContext.SignInAsync(
                             scheme: "FiverSecurityScheme",
                             principal: principal);
                

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

      
    }
}