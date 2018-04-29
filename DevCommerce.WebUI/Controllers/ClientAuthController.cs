using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevCommerce.WebUI.Controllers
{
    [Authorize]
    public class ClientAuthController : ClientBaseController
    {
       
    }
}