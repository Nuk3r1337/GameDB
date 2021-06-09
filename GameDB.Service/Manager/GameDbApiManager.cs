using GameDB.Domain.DomainClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace GameDB.Service
{
    public interface IGameDbApiManager
    {
        Task<User> GetUser(int Id);
        Task<HttpStatusCode> CreateUser(User user);
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
        Task<HttpStatusCode> CreatePublisher(Publisher publisher);
        Task<HttpStatusCode> CreateAgeRating(AgeRating ageRating);
        Task<HttpStatusCode> CreateGenre(Genre genre);
        Task<HttpStatusCode> CreateRole(Role role);
        Task<List<ExternalGame>> GetExternalGame(string code);

        Task<Search> GetSearchResult(string input, string function);
    }
    public class GameDbApiManager : IGameDbApiManager
    {
        private readonly HttpClient httpClient;

        public GameDbApiManager(IAppSettings appSettings)
        {
            this.httpClient = CreateHttpClientAsync(appSettings);
        }

        private static HttpClient CreateHttpClientAsync(IAppSettings appSettings)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri(appSettings.ApiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            return client;
        }

        public async Task<HttpStatusCode> CreateAgeRating(AgeRating ageRating)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/", ageRating);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> CreateComment(Comment comment)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/", comment);
                response.EnsureSuccessStatusCode();
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

        public async Task<HttpStatusCode> CreateGenre(Genre genre)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/category", genre);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> CreatePublisher(Publisher publisher)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/category", publisher);
                response.EnsureSuccessStatusCode();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
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

        public async Task<HttpStatusCode> CreateUser(User user)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "insert.php/", user);
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
                HttpResponseMessage response = await httpClient.DeleteAsync($"edit.php/update/delete/{Id}");
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
                HttpResponseMessage response = await httpClient.DeleteAsync($"edit.php/update/delete/{Id}");
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
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/api/games/" + name);
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
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/api/games/" + Id);
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
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/api/users/" + Id);
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
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"edit.php/update/update/", comment);
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
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(httpClient.BaseAddress + "/api/games/", game);
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
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/api/barcode/" + code);
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
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress + "/api/barcode", barcode);
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
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(httpClient.BaseAddress + "/api/barcode", barcode);
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

        public async Task<Search> GetSearchResult(string input, string function)
        {
            try
            {
                Search search = new Search();
                switch(function)
                {
                    case "User":
                        List<User> users = null;
                        HttpResponseMessage response = await httpClient.GetAsync( httpClient.BaseAddress + "/api/users/" + input);
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                users = await content.ReadFromJsonAsync<List<User>>();
                                search.Users = users;
                                return search;
                            }
                        }
                        return null;

                    case "Game":
                        List<Game> game = null;
                        HttpResponseMessage gameResponse = await httpClient.GetAsync(httpClient.BaseAddress + "/api/games/" + input);
                        if (gameResponse.IsSuccessStatusCode)
                        {
                            using (HttpContent content = gameResponse.Content)
                            {
                                game = await content.ReadFromJsonAsync<List<Game>>();
                                search.Games = game;
                                return search;
                            }
                        }
                        return null;
                }
                return null;
            }
            catch(Exception)
            {
                return null;
            }

        }
    }
}
