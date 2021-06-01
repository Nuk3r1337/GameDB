using GameDB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;

namespace GameDB.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameDbApiManager gameManager;

        public GameController(IGameDbApiManager gameManager)
        {
            this.gameManager = gameManager;
        }
        public async Task<IActionResult> GameIndexAsync(int GameID)
        {
            var game = await gameManager.GetGame(GameID);
            return View(game);
        }
        [HttpGet]
        public async Task<IActionResult> EditGameAsync(int GameID)
        {
            var game = await gameManager.GetGame(GameID);
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> EditGame(Game game)
        {
            try
            {
                var edits = await gameManager.UpdateGame(game);
                if(edits != null)
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = game.Id});
                }
                else
                {
                    return RedirectToAction("EditGame", "Game", new { GameID = game.Id });
                }
            }
            catch(Exception)
            {
                return View(game.Id);
            }
        }
    }
}
