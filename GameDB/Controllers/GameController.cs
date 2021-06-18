using GameDB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;
using System.Net;
using GameDB.Service.Manager;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditGameAsync(int GameID)
        {
            GameEdit Ge = new GameEdit();
            Ge.game = await gameManager.GetGame(GameID);
            Ge.AgeRatings = await gameManager.GetAgeRating();
            Ge.Publishers = await gameManager.GetPublishers();
            Ge.Genres = await gameManager.GetGenres();

            return View(Ge);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditGame(Game game, List<int> genreCheck)
        {
            try
            {
                var edits = await gameManager.UpdateGame(game);
                if(edits != null)
                {
                    foreach(int id in genreCheck)
                    {
                        Game_Has_Genre ghg = new Game_Has_Genre { Games_id = game.Id, Genre_id = id };
                        var addGenre = await gameManager.CreateGenreForGame(ghg);
                    }
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

        public async Task<bool> PostCommentAsync(string comment, int game)
        {
            try
            {
                int userID = int.Parse(HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier).Value);
                Comment insert = new Comment { Comments = comment, games_id = game, users_id = userID };
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

        public async Task<bool> DeleteCommentAsync(int commentID)
        {
            try
            {
                var response = await gameManager.DeleteComment(commentID);
                if(response == HttpStatusCode.OK)
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
                int userID = int.Parse(HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier).Value);
                Insert_User_Games insert = new Insert_User_Games { Users_id = userID, Games_id = GameID };
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGame(int GameID)
        {
            try
            {
                var status = await gameManager.DeleteGame(GameID);
                if(status == HttpStatusCode.OK)
                {
                    return RedirectToAction("SearchIndex", "Search");
                }
                else
                {
                    return RedirectToAction("EditGame", "Game", new { GameID = GameID });
                }
            }
            catch(Exception)
            {
                return RedirectToAction("EditGame", "Game", new { GameID = GameID });
            }
        }
    }
}
