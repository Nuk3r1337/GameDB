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
        public async Task<IActionResult> IndexAsync(string searchInput = "")
        {
            var ok = await _gameManager.SearchGame(searchInput);

            return View(ok);
        }
    }
}
