using GameDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameDB.Service;
using GameDB.Domain.DomainClasses;
using System.Net;
using ZXing;
using System.IO;
using System.Drawing;
namespace GameDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameDbApiManager _gameManager;
        public HomeController(ILogger<HomeController> logger, IGameDbApiManager gameManager)
        {
            _gameManager = gameManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendAsync(string Barcode)
        {
            try
            {
                //Check if barcode exists
                var game = await _gameManager.GetBarcode(Barcode);
                if(game == null)
                {
                    //Barcode does not exist, get game data from another API
                    var externalGame = await _gameManager.GetExternalGame(Barcode);

                    //Get the informations and create an entry
                    Game gameObj = new Game
                    {
                        Title = externalGame.FirstOrDefault().Title
                    };
                    var newGame = await _gameManager.CreateGame(gameObj);

                    if(newGame != null)
                    {
                        //Create a barcode entry for the new game entry
                        InsertBarcode createBarcode = new InsertBarcode { Code = Barcode, Games_id = newGame.Id };
                        var newBarcode = await _gameManager.CreateBarcode(createBarcode);
                        if(newBarcode == HttpStatusCode.Created)
                        {
                            return RedirectToAction("GameIndex", "Game", new { GameID = newGame.Id });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("GameIndex", "Game", new { GameID = game.Games_id.Id});
                }
                
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //Scanning Barcode
        public string scanCamPic(string imageData)
        {
            byte[] bytearray = Convert.FromBase64String(imageData);
            string result = ReadQrCode(bytearray);
            return result;
        }
        public static string ReadQrCode(byte[] qrCode)
        {
            BarcodeReader coreCompatReader = new BarcodeReader();

            using (Stream stream = new MemoryStream(qrCode))
            {
                using (var coreCompatImage = (Bitmap)Image.FromStream(stream))
                {

                    if (coreCompatReader.Decode(coreCompatImage) is null)
                    {
                        return "fail";
                    }
                    else
                    {
                        return coreCompatReader.Decode(coreCompatImage).Text;
                    }
                }
            }
        }

        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
