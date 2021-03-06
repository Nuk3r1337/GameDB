using GameDB.Domain.DomainClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ZXing;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Flurl;

namespace GameDB.Service.Manager
{
    public interface IGameDbApiManager
    {
        Task<User> GetUser(int Id);
        Task<User> UpdateUser(User user);
        Task<HttpStatusCode> DeleteUser(int Id);
        Task<Game> CreateGame(Game game);
        Task<List<Game>> SearchGame(string name);
        Task<Game> GetGame(int Id);
        Task<Game> UpdateGame(Game game);
        Task<HttpStatusCode> DeleteGame(int Id);
        Task<List<Comment>> GetComments(int gameId);
        Task<HttpStatusCode> DeleteComment(int Id);
        Task<Comment> UpdateComment(Comment comment);
        Task<HttpStatusCode> CreateComment(Comment comment);
        Task<Barcode> GetBarcode(string code);
        Task<HttpStatusCode> CreateBarcode(InsertBarcode barcode);
        Task<Barcode> UpdateBarcode(Barcode barcode);
        Task<HttpStatusCode> DeleteBarcode(string code);
        Task<HttpStatusCode> CreateRole(Role role);
        Task<List<ExternalGame>> GetExternalGame(string code);
        string ReadQrCode(byte[] qrCode);
        Task<HttpStatusCode> CreateUserGames(Insert_User_Games user_Games);
        Task<User> GetUserGames(int userId);
        Task<HttpStatusCode> DeleteUserGames(Insert_User_Games user_Games);
        Task<HttpStatusCode> CreateUserRating(User_Rating user_rating);
        Task<List<Publisher>> GetPublishers();
        Task<List<AgeRating>> GetAgeRating();
        Task<List<Genre>> GetGenres();
        Task<HttpStatusCode> CreateGenreForGame(Game_Has_Genre game);
    }
    public class GameDbApiManager : IGameDbApiManager
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GameDbApiManager(IAppSettings appSettings, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = CreateHttpClientAsync(appSettings);
        }

        private HttpClient CreateHttpClientAsync(IAppSettings appSettings)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri(appSettings.ApiUrl);
            var accessToken = httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return client;
        }

        public async Task<HttpStatusCode> CreateComment(Comment comment)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri + "/api/comments");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, comment);
                //response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch(Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<Game> CreateGame(Game game)
        {
            try
            {
                Game newgame = null;
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "/api/games", game);
                if(response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        newgame = await content.ReadFromJsonAsync<Game>();
                    }
                }
                return newgame;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> CreateRole(Role role)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/", role);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }
        public async Task<HttpStatusCode> DeleteComment(int Id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/api/comments/{Id}");
                return response.StatusCode;
            }
            catch(Exception)
            {
                return HttpStatusCode.BadRequest;
            }
            
        }

        public async Task<HttpStatusCode> DeleteGame(int Id)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress + $"/api/games/{Id}");
                HttpResponseMessage response = await httpClient.DeleteAsync(url);
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }

        }

        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"edit.php/update/delete/{Id}");
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<List<Comment>> GetComments(int gameId)
        {
            try
            {
                List<Comment> comment = null;
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "search.php/notes=" + gameId);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        comment = await content.ReadFromJsonAsync<List<Comment>>();
                    }
                }
                return comment;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public async Task<List<Game>> SearchGame(string name)
        {
            try
            {
                List<Game> game = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/games/" + name);
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + url);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        game = await content.ReadFromJsonAsync<List<Game>>();
                    }
                }
                return game;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Game> GetGame(int Id)
        {
            try
            {
                Game game = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/games/" + Id);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        game = await content.ReadFromJsonAsync<Game>();
                    }
                }
                return game;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<User> GetUser(int Id)
        {
            try
            {
                List<User> user = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/users/" + Id);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        user = await content.ReadFromJsonAsync<List<User>>();
                    }
                }
                return user[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "edit.php/update/update/");
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(url, comment);
                response.EnsureSuccessStatusCode();

                comment = await response.Content.ReadFromJsonAsync<Comment>();
                return comment;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Game> UpdateGame(Game game)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/games/" + game.Id);
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(url, game);
                response.EnsureSuccessStatusCode();
                var code = response.StatusCode;
                game = await response.Content.ReadFromJsonAsync<Game>();
                return game;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"edit.php/update/update/", user);
                response.EnsureSuccessStatusCode();

                user = await response.Content.ReadFromJsonAsync<User>();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Barcode> GetBarcode(string code)
        {
            try
            {
                Barcode barcode = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/barcode/" + code);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        barcode = await content.ReadFromJsonAsync<Barcode>();
                        
                    }
                }
                return barcode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> CreateBarcode(InsertBarcode barcode)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/barcode");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, barcode);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<Barcode> UpdateBarcode(Barcode barcode)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/barcode");
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(url, barcode);
                response.EnsureSuccessStatusCode();

                barcode = await response.Content.ReadFromJsonAsync<Barcode>();
                return barcode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> DeleteBarcode(string code)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"edit.php/update/delete/{code}");
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<List<ExternalGame>> GetExternalGame(string code)
        {
            try
            {
                ExternalGameList externalGame = null;
                List<ExternalGame> game = null;
                HttpResponseMessage response = await httpClient.GetAsync("https://api.upcitemdb.com/prod/trial/lookup?upc=" + code);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        externalGame = await content.ReadFromJsonAsync<ExternalGameList>();
                        game = externalGame.Items;
                    }
                }
                return game;
            }
            catch(Exception)
            {
                return null;
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
        
        public string ReadQrCode(byte[] qrCode)
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

        public async Task<HttpStatusCode> CreateUserGames(Insert_User_Games user_Games)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/usergames");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, user_Games);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> CreateUserRating(User_Rating user_rating)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/InsertHere");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, user_rating);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<User> GetUserGames(int userId)
        {
            try
            {
                User games = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/user/" + userId + "/games");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        games = await content.ReadFromJsonAsync<User>();
                        return games;
                    }
                }
                return null;

            }
            catch(Exception)
            {
                return null;
            }
        }
        public Task<HttpStatusCode> DeleteUserGames(Insert_User_Games user_Games)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            try
            {
                List<Publisher> publisher = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/publishers");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                using (HttpContent content = Response.Content)
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        publisher = await content.ReadFromJsonAsync<List<Publisher>>();
                        return publisher;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AgeRating>> GetAgeRating()
        {
            try
            {
                List<AgeRating> ages = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/ageratings");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                using (HttpContent content = Response.Content)
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        ages = await content.ReadFromJsonAsync<List<AgeRating>>();
                        return ages;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Genre>> GetGenres()
        {
            try
            {
                List<Genre> genres = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/genres");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                using (HttpContent content = Response.Content)
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        genres = await content.ReadFromJsonAsync<List<Genre>>();
                        return genres;
                    }
                }
                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> CreateGenreForGame(Game_Has_Genre game)
        {
            try
            {
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/gamegenres");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, game);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

    }
}
