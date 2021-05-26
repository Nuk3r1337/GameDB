using GameDB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDB.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameDbApiManager gameManager;

        public GameController(IGameDbApiManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public async Task<IActionResult> IndexAsync(int GameID)
        {
            var game = await gameManager.GetGame(GameID);
            return View(game);
        }
    }
}
