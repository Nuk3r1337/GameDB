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
using ZXing;
using System.IO;
using System.Drawing;
using GameDB.Service.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        [HttpGet("~/logout")]
        [HttpPost("~/logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> SendAsync(string Barcode)
        {
            try
            {
                //Check if barcode exists
                var game = await _gameManager.GetBarcode(Barcode);
                if(game == null)
                {
                    //Barcode does not exist, get game data from another API
                    var externalGame = await _gameManager.GetExternalGame(Barcode);

                    if(externalGame != null)
                    {
                    //Get the informations and create an entry
                        Game gameObj = new Game
                        {
                            Title = externalGame.FirstOrDefault().Title
                        };
                        var newGame = await _gameManager.CreateGame(gameObj);

                        if(newGame != null)
                        {
                            //Create a barcode entry for the new game entry
                            InsertBarcode createBarcode = new InsertBarcode { Code = Barcode, Games_id = newGame.Id };
                            var newBarcode = await _gameManager.CreateBarcode(createBarcode);
                            if(newBarcode == HttpStatusCode.Created)
                            {
                                return RedirectToAction("GameIndex", "Game", new { GameID = newGame.Id });
                            }
                        }
                    }

                    return RedirectToAction("Index");
                    
                }
                else
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = game.Games_id.Id});
                }
                
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //Scanning Barcode
        public string scanCamPic(string imageData)
        {
            byte[] bytearray = Convert.FromBase64String(imageData);
            string result = _gameManager.ReadQrCode(bytearray);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
