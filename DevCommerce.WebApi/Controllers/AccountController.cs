using AutoMapper;
using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Email;
using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevCommerce.WebApi.Controllers
{
    [Route("api/Account")]
    public class AccountController : AccountBaseController
    {
        //private readonly IMapper _mapper;
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ITokenService _tokenService;

        //public JwtTokenParameter JwtTokenParameter { get; }
        //public EmailParameter EmailParameter { get; }

        //public AccountController(
        //    IMapper mapper,
        //    UserManager<User> userManager,
        //    SignInManager<User> signInManager,
        //    ITokenService tokenService,
        //    IEmailSender emailSender,
        //    IOptions<JwtTokenParameter> jwtTokenParameter,
        //    IOptions<EmailParameter> emailParameter):base(mapper,userManager,signInManager,tokenService,emailSender,jwtTokenParameter,emailParameter)
        //{

        //}
        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailSender emailSender, IOptions<JwtTokenParameter> jwtTokenParameter, IOptions<EmailParameter> emailParameter) : base(mapper, userManager, signInManager, tokenService, emailSender, jwtTokenParameter, emailParameter)
        {
        }
    }
}

