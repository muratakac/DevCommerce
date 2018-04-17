using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.CrossCuttingConcerns.Security;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Extensions;
using DevCommerce.Web.Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;

        public JwtTokenParameter JwtTokenParameter { get; }
        public EmailParameter EmailParameter { get; }

        public AccountBaseController(
            IMapper mapper,
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
            _mapper = mapper;
        }

        /// <summary>
        /// New User
        /// </summary>
        /// <param name="model">
        /// url/api/Account/Register
        /// Header
        /// Authorization : Bearer TokenValue
        /// Body
        /// {"UserName":"?","Email":"?","Password":"?","ConfirmPassword":"?"}
        /// </param>
        /// <returns></returns>
        [Authorize()]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //User user = _mapper.Map<RegisterViewModel, User>(model);
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(EmailParameter.DisplayName, EmailParameter.MailAddress, model.Email, "Confirm your email", callbackUrl);
                return new OkObjectResult("Account created");
            }

            return BadRequest("Hatalı istek");
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model">
        /// url/api/Account/Login
        /// Header
        /// Authorization : Bearer TokenValue
        /// Body
        /// { "Email":"?","Password":"?","RememberMe":"false"}
        /// </param>
        /// <returns></returns>
        [Authorize()]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        return BadRequest("Kilitli hesap");
                    }
                    else
                    {
                        return BadRequest("Hatalı içerik");
                    }
                }
            }

            return BadRequest("Hatalı içerik");
        }

        [Authorize()]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Hatalı içerik");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Hatalı içerik");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="model">url/api/Account/GetToken
        /// {"CompanyName":"?","ProjectName":"?","TokenKey":"?","TokenValue":"?"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> CreateToken([FromBody]TokenViewModel model)
        {
            Token tokenModel = _mapper.Map<TokenViewModel, Token>(model);
            if (_tokenService.CheckToken(tokenModel))
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

                return Ok(result);
            }

            return BadRequest();
        }

    }
}