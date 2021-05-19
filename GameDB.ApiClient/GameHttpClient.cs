using System;

namespace GameDB.ApiClient
{
    public interface IApiService
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
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient, IConfig config)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(config.ApiUrl);
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

        public Task<Game> GetGame(int Id)
        {
            throw new NotImplementedException();
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
