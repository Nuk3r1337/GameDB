using GameDB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;
using System.Net;

namespace GameDB.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameDbApiManager gameManager;
        private int gameID;
        public GameController(IGameDbApiManager gameManager)
        {
            this.gameManager = gameManager;
        }
        public async Task<IActionResult> GameIndexAsync(int GameID)
        {
            var game = await gameManager.GetGame(GameID);
            if(game.User_Ratings != null)
            {
                int rating = 0;
                foreach(var rate in game.User_Ratings)
                {
                    rating += rate.Rating;
                }
                ViewBag.rateMath = rating / game.User_Ratings.Count();
            }
            gameID = game.Id;
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
    
        public async Task<bool> PostCommentAsync(string comment)
        {
            try
            {
                Comment insert = new Comment { Comments = comment, Game = gameID };
                var api = await gameManager.CreateComment(insert);
                if(api == HttpStatusCode.Created)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
