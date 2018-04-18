using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Controllers;
using DevCommerce.Web.Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DevCommerce.WebApi.Controllers
{
    [Route("api/Account")]
    public class AccountController : AccountBaseController
    {
        private readonly IMapper _mapper;
     
        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter) : base(userManager, signInManager, tokenService, emailSender, jwtTokenParameter, emailParameter)
        {
            _mapper = mapper;
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
            var result = await base.CreateTokenAsync(tokenModel);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Token oluşturulamadı...");
            }
            else
            {
                return Ok(result);
            }
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
                var result = await base.LoginAsync(model);

                if (result)
                {
                    return Ok("Giriş başarılı");
                }
                else
                {
                    return BadRequest("Lütfen bilgilerinizi kontrol edin...");
                }
            }

            return BadRequest("Hatalı içerik");
        }

        [Authorize()]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await base.LogoutAsync();
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
                return BadRequest(ModelState);
            }

            User user = _mapper.Map<RegisterViewModel, User>(model);
            var result = await base.RegisterAsync(user, model.Password);
            if (result)
            {
                return new OkObjectResult("Yeni kullanıcı oluşturuldu.");
            }

            return BadRequest("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Lütfen bilgilerinizi kontrol edin.");
            }

            var result = await base.ConfirmEmailAsync(userId, code);
            if (result)
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

            var result = await base.ResetPasswordAsync(model);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}

