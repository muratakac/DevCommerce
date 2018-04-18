using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.CrossCuttingConcerns.Security;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Extensions;
using DevCommerce.Web.Framework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DevCommerce.Web.Framework.Controllers
{
    /*
     References
         https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x
         https://github.com/aspnet/Docs/blob/master/aspnetcore/security/authentication/identity/sample/src/ASPNETCore-IdentityDemoComplete/IdentityDemo/Controllers/AccountController.cs
         */


    public class AccountBaseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;

        public JwtTokenParameter JwtTokenParameter { get; }
        public EmailParameter EmailParameter { get; }

        public AccountBaseController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IEmailSender emailSender,
            IOptions<JwtTokenParameter> jwtTokenParameter,
            IOptions<EmailParameter> emailParameter)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            JwtTokenParameter = jwtTokenParameter.Value;
            EmailParameter = emailParameter.Value;
        }


        public async Task<bool> RegisterAsync(User model, string password)
        {
            var result = await _userManager.CreateAsync(model, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(model, isPersistent: false);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(model);
                var callbackUrl = Url.EmailConfirmationLink(model.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(EmailParameter.DisplayName, EmailParameter.MailAddress, model.Email, "Confirm your email", callbackUrl);

                return true;
            }

            return false;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            return result.Succeeded;
        }

        public async Task<string> CreateTokenAsync(Token model)
        {
            if (_tokenService.CheckToken(model))
            {
                var token = new JwtTokenBuilder()
                              .AddSecurityKey(JwtSecurityKey.Create("dev-security-value-wep-api"))
                              .AddSubject(string.Concat(model.CompanyName, "_", model.ProjectName))
                              .AddIssuer(JwtTokenParameter.Issuer)
                              .AddAudience(JwtTokenParameter.Audience)
                              .AddClaim(model.TokenKey, model.TokenValue)
                              .AddExpiry(1)
                              .Build();

                var result = await Task.FromResult<string>(token.Value);

                return result;
            }

            return string.Empty;
        }
    }
}