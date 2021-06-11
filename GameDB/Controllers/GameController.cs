﻿using GameDB.Service;
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
    
        [HttpPost]
        public async Task<IActionResult> AddUserRating(int user_Rating)
        {
            try
            {
                User_Rating userR = new User_Rating { Rating = user_Rating };
                var status = await gameManager.CreateUserRating(userR);
                if(status == HttpStatusCode.Created)
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = gameID });
                }
                else
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = gameID });
                }
            }
            catch(Exception)
            {
                return RedirectToAction("GameIndex", "Game", new { GameID = gameID });
            }
        }
    
        public async Task<IActionResult> AddToLibrary(int GameID)
        {
            try
            {
                Insert_User_Games insert = new Insert_User_Games { Users_id = 1, Games_id = GameID };
                var status = await gameManager.CreateUserGames(insert);
                if (status == HttpStatusCode.Created)
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = GameID });
                }
                else
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = GameID });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("GameIndex", "Game", new { GameID = GameID });
            }
        }
    }
}