using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCommerce.Entities.Concrete;
using DevCommerce.WebApi.Models;
using DevCommerce.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevCommerce.WebApi.Services;

namespace DevCommerce.WebApi.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x
    //https://github.com/aspnet/Docs/blob/master/aspnetcore/security/authentication/identity/sample/src/ASPNETCore-IdentityDemoComplete/IdentityDemo/Controllers/AccountController.cs
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
           UserManager<User> userManager,
           SignInManager<User> signInManager,
             IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                //_logger.LogInformation("User created a new account with password.");

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                await _signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation("User created a new account with password.");
                return new OkObjectResult("Account created");
            }

            AddErrors(result);

            return BadRequest();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Account/Login")]
        //public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            //_logger.LogInformation("User logged in.");
        //            return Ok();
        //        }

        //        if (result.IsLockedOut)
        //        {
        //            //_logger.LogWarning("User account locked out.");
        //            return BadRequest();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    //_logger.LogInformation("User logged out.");
        //    return Ok();
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return BadRequest();
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        //throw new ApplicationException($"Unable to load user with ID '{userId}'.");
        //        return BadRequest();
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    return Ok();
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string code = null)
        //{
        //    if (code == null)
        //    {
        //        //throw new ApplicationException("A code must be supplied for password reset.");
        //        return BadRequest();
        //    }
        //    var model = new ResetPasswordViewModel { Code = code };
        //    return View(model);
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete([FromBody]string id)
        //{
        //    User user = await _userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        var result = await _userManager.DeleteAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return Ok();
        //        }
        //    }
        //    return BadRequest();
        //}
    }
}