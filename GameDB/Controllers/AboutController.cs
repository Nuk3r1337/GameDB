using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDB.Controllers
{
    public class AboutController : Controller
    {
        [Authorize]
        public IActionResult AboutIndex()
        {
            return View();
        }
    }
}
