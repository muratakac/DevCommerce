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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevCommerce.WebUI.Controllers
{
    public class AccountController : AccountBaseController
    {
        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter) : base(mapper, userManager, signInManager, tokenService, emailSender, jwtTokenParameter, emailParameter)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}