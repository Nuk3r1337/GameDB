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
using System.Net;

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
                //Check if barcode exists
                var game = await _gameManager.GetBarcode(Barcode);
                if(game.Count() == 0)
                {
                    //Barcode does not exist, get game data from another API
                    var externalGame = await _gameManager.GetExternalGame(Barcode);

                    //Get the informations and create an entry
                    Game gameObj = new Game { Title = externalGame.FirstOrDefault().Title };
                    var newGame = await _gameManager.CreateGame(gameObj);

                    if(newGame == HttpStatusCode.Created)
                    {
                        //Create a barcode entry for the new entry
                        var searchGame = await _gameManager.SearchGame(externalGame.FirstOrDefault().Title);
                        Barcode createBarcode = new Barcode { Code = Barcode, Game = new Game { Id = searchGame.FirstOrDefault().Id } };
                        var newBarcode = await _gameManager.CreateBarcode(createBarcode);
                        if(newBarcode == HttpStatusCode.Created)
                        {
                            return RedirectToAction("GameIndex", "Game", new { GameID = searchGame.FirstOrDefault().Id });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
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
