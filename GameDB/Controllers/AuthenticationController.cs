using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDB.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {

        [HttpGet("Logon")]
        public IActionResult AuthenticationLogon()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/" });
        }
    }
}
