using GameDB.Domain.DomainClasses;
using GameDB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDB.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGameDbApiManager _gameManager;
        public SearchController(IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
        }
        public async Task<IActionResult> SearchIndexAsync(string searchInput = "", string searchType = "gameTitles")
        {
            switch(searchType)
            {
                case "gameTitles":
                    var gameTitle = await _gameManager.GetSearchResult(searchInput, "Game");
                    ViewBag.Table = "GameTable";
                    return View(gameTitle);
                case "usernames":
                    var username = await _gameManager.GetSearchResult(searchInput, "User");
                    ViewBag.Table = "UserTable";
                    return View(username);
                case "gamePublishers":
                    var publishers = await _gameManager.GetSearchResult(searchInput, "Publishers");
                    ViewBag.Table = "PubTable";
                    return View(publishers);
                case "ageRating":
                    var ageRating = await _gameManager.GetSearchResult(searchInput, "AgeRatings");
                    ViewBag.Table = "Ages";
                    return View(ageRating);
                case "releaseDate":
                    var release = await _gameManager.GetSearchResult(searchInput, "ReleaseDate");
                    ViewBag.Table = "Release";
                    return View(release);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
