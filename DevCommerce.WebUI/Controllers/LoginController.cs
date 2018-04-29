using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevCommerce.Entities.Concrete;
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
                return View("Index", "Home");


            string stringData = ClientBaseController.ServiceGetData("/api/Account/Login", RequestType.POST, JsonConvert.SerializeObject(loginViewModel), true);
            var user = JsonConvert.DeserializeObject<User>(stringData);

            if (user != null)
            {
                var claims = new List<Claim> {
                                new Claim("Email", user.Email),
                                new Claim("FullName", string.Format("{0} {1}", user.FirstName, user.LastName))
                            };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                            scheme: "FiverSecurityScheme",
                            principal: principal);


                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (User.Identity.IsAuthenticated)
                return View("Index", "Home");

            string stringData = ClientBaseController.ServiceGetData("/api/Account/Register", RequestType.POST, JsonConvert.SerializeObject(registerViewModel), true);
            var result = (bool)JsonConvert.DeserializeObject(stringData);
            if (result)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

    }
}