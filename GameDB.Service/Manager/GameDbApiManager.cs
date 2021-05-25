using GameDB.Domain.DomainClasses;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameDB.Service
{
    public interface IGameDbApiManager
    {
        Task<User> GetUser(int Id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int Id);
        Task<Game> CreateGame(Game game);
        Task<Game> GetGame(int Id);
        Task<Game> UpdateGame(Game game);
        Task<Game> DeleteGame(int Id);
        Task<Comment> GetComments(int gameId);
        Task<Comment> DeleteComment(int Id);
        Task<Comment> UpdateComment(Comment comment);
        Task<Comment> CreateComment(Comment comment);
        Task<Publisher> CreatePublisher(Publisher publisher);
        Task<AgeRating> CreateAgeRating(AgeRating ageRating);
        Task<Genre> CreateGenre(Genre genre);
        Task<Role> CreateRole(Role role);
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

        public Task<AgeRating> CreateAgeRating(AgeRating ageRating)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Game> CreateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<Genre> CreateGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Publisher> CreatePublisher(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public Task<Role> CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> DeleteComment(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> DeleteGame(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetComments(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetGame(int Id)
        {
            try
            {
                Game game = null;
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "search.php/itemId=" + Id);
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

        public Task<User> GetUser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
