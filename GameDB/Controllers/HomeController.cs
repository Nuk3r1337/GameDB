using GameDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Service;
using GameDB.Domain.DomainClasses;

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
            return View();
        }

        public async Task<IActionResult> SendAsync(string Barcode)
        {
            try
            {
                var game = await _gameManager.GetBarcode(Barcode);
                if(game.Count() == 0)
                {
                    var externalGame = await _gameManager.GetExternalGame(Barcode);
                    //CreateGame(externalGame[0].Title, Barcode);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = game[0].Id});
                }
                
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //private void CreateGame(string gameTitle, string barcode)
        //{
        //    Game game = new Game 
        //    { 
        //        Title = gameTitle
        //    };
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
