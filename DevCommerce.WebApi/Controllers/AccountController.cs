using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.CrossCuttingConcerns.Security;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DevCommerce.WebApi.Controllers
{
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;

        public JwtTokenParameter JwtTokenParameter { get; }
        public EmailParameter EmailParameter { get; }

        //public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter, IStringLocalizer stringLocalizer)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _tokenService = tokenService;
        //    _emailSender = emailSender;
        //    _mapper = mapper;
        //    _stringLocalizer = stringLocalizer;

        //    JwtTokenParameter = jwtTokenParameter.Value;
        //    EmailParameter = emailParameter.Value;
        //}

        public AccountController(IMapper mapper,  ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter, IStringLocalizer stringLocalizer)
        {
            _tokenService = tokenService;
            _emailSender = emailSender;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;

            JwtTokenParameter = jwtTokenParameter.Value;
            EmailParameter = emailParameter.Value;
        }

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="model">url/api/Account/GetToken
        /// {"CompanyName":"?","ProjectName":"?","TokenKey":"?","TokenValue":"?"}</param>
        /// <returns></returns>
        //[HttpPost]
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

            return BadRequest(_stringLocalizer.GetString("Account_Login_Token_Error"));
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
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    User user = _userManager.FindByNameAsync(model.UserName).Result;
                    if (user!=null)
                    {
                        return Ok(user);
                    }

                    return Ok(null);
                }
                //else
                //{
                //    if (result.IsLockedOut)
                //    {
                //        return BadRequest(_stringLocalizer.GetString("Account_Login_Lock_Error"));
                //    }
                //    else
                //    {
                //        return BadRequest(_stringLocalizer.GetString("Account_Login_Data_Error"));
                //    }
                //}
            }

            // return BadRequest(_stringLocalizer.GetString("Account_Login_Data_Error"));
            return BadRequest(null);
        }

        [Authorize()]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
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
                return BadRequest(false);
            }

            User user = _mapper.Map<RegisterViewModel, User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //Send Email
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailConfirmationAsync(EmailParameter.DisplayName, EmailParameter.MailAddress, model.Email, "Confirm your email", callbackUrl);
                //return new OkObjectResult(_stringLocalizer.GetString("Account_Register_Success_Message"));
                return Ok(true);
            }

            //return BadRequest(_stringLocalizer.GetString("Account_Register_Data_Error"));
            return BadRequest(false);
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(_stringLocalizer.GetString("Account_ConfirmEmail_Data_Error"));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException(string.Format(_stringLocalizer.GetString("Account_ConfirmEmail_User_Not_Found_Error"), userId));
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
                return BadRequest(_stringLocalizer.GetString("Account_ResetPassword_Data_Error"));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}

