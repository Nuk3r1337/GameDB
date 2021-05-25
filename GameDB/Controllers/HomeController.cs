using GameDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Service;

namespace GameDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameDbApiManager _gameManager;
        public HomeController(ILogger<HomeController> logger, IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var ok = _gameManager.GetGame(1);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
