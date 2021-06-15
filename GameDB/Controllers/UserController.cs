using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Service;
using GameDB.Domain.DomainClasses;
using System.Net;
using GameDB.Service.Manager;
using Microsoft.AspNetCore.Authorization;

namespace GameDB.Controllers
{
    public class UserController : Controller
    {
        private readonly IGameDbApiManager _gameManager;
        public UserController(IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
        }
        public async Task<IActionResult> UserIndexAsync(int UserID)
        {
            //var user = await _gameManager.GetUser(UserID);
            var user = await _gameManager.GetUserGames(UserID);
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditUserAsync(int UserID)
        {
            var user = await _gameManager.GetUser(UserID);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(User user)
        {
            try
            {
                if(user.Password == user.ConfirmPassword)
                {
                    var result = await _gameManager.UpdateUser(user);
                    return RedirectToAction("UserIndex", "User", new { UserID = result.Id });
                }
                else
                {
                    return View(user);
                }
            }
            catch(Exception)
            {
                return View(user);
            }
        }
    }
}
