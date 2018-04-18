using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Controllers;
using DevCommerce.Web.Framework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace DevCommerce.WebUI.Controllers
{
    public class AccountController : AccountBaseController
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;
        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter, IStringLocalizer localizer) : base( userManager, signInManager, tokenService, emailSender, jwtTokenParameter, emailParameter)
        {
            _mapper = mapper;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public IActionResult CreateToken(TokenViewModel model)
        {
            Token tokenModel = _mapper.Map<TokenViewModel, Token>(model);
            var result =  base.CreateTokenAsync(tokenModel);
            if (string.IsNullOrEmpty(result.Result))
            {
                return BadRequest(_localizer.GetString("NotCreatedToken"));
            }
            else
            {
                return Ok(result);
            }
        }
    }
}