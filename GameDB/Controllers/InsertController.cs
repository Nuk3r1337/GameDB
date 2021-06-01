using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;
using GameDB.Service;
using System.Net;

namespace GameDB.Controllers
{
    public class InsertController : Controller
    {
        private readonly IGameDbApiManager _gameManager;
        public InsertController(IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PublisherAsync(Publisher publisher)
        {
            try
            {
                //Publisher publisher = new Publisher { Name = pubName };
                var url = await _gameManager.CreatePublisher(publisher);
                if(url == HttpStatusCode.Created)
                {
                    //Do something for success
                    return RedirectToAction("Index");
                }
                else
                {
                    //Do something for failure
                    return RedirectToAction("Index");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
