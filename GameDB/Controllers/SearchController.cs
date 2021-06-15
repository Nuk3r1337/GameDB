using GameDB.Domain.DomainClasses;
using GameDB.Service.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace GameDB.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGameDBSearchManager _searchManager;
        private readonly IGameDbApiManager _gameManager;
        public SearchController(IGameDBSearchManager searchManager, IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
            _searchManager = searchManager;
        }
        public async Task<IActionResult> SearchIndexAsync(string searchInput = "", string searchType = "gameTitles")
        {

            Search search = new Search();

            search.Publishers = await _gameManager.GetPublishers();
            search.Genres = await _gameManager.GetGenres();
            search.AgeRatings = await _gameManager.GetAgeRating();

            switch(searchType)
            {
                case "gameTitles":
                    search.Games = await _searchManager.GetSearchGame(searchInput);
                    ViewBag.Table = "GameTable";
                    return View(search);

                case "usernames":
                    search.Users = await _searchManager.GetUser(searchInput);
                    ViewBag.Table = "UserTable";
                    return View(search);

                case "gamePublishers":
                    search.Games = await _searchManager.GetPublisherGame(searchInput);
                    ViewBag.Table = "GameTable";
                    return View(search);

                case "ageRating":
                    search.Games = await _searchManager.GetAgeRatingGame(searchInput);
                    ViewBag.Table = "GameTable";
                    return View(search);

                //case "releaseDate":
                //    search = await _searchManager.GetSearchResult(searchInput, "ReleaseDate");
                //    ViewBag.Table = "GameTable";
                //    return View(search);

                case "genre":
                    search.Games = await _searchManager.GetGenreGame(searchInput);
                    ViewBag.Table = "GameTable";
                    return View(search);
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<bool> AddToGameList(int gameId)
        {
            int userID = int.Parse(HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier).Value);
            Insert_User_Games insert = new Insert_User_Games { Users_id = userID, Games_id = gameId };
            var statusCode = await _gameManager.CreateUserGames(insert);
            if(statusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
