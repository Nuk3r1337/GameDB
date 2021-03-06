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
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Flurl;

namespace GameDB.Service.Manager
{
    public interface IGameDBSearchManager
    {
        Task<List<Game>> GetPublisherGame(string input);
        Task<List<Game>> GetAgeRatingGame(string input);
        Task<List<Game>> GetGenreGame(string input);
        Task<List<Game>> GetSearchGame(string input);
        Task<List<User>> GetUser(string input);

    }

    public class GameDBSearchManager : IGameDBSearchManager
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GameDBSearchManager(IAppSettings appSettings, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<Game>> GetAgeRatingGame(string input)
        {
            try
            {
                List<Game> game = null;
                AgeRating age = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, $"/api/agerating/{input}/games/");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    using (HttpContent content = Response.Content)
                    {
                        age = await content.ReadFromJsonAsync<AgeRating>();
                        game = age.game;
                        return game;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Game>> GetGenreGame(string input)
        {
            try
            {
                List<Game> game = null;
                Genre genre = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, $"/api/genre/{input}/games/");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    using (HttpContent content = Response.Content)
                    {
                        genre = await content.ReadFromJsonAsync<Genre>();
                        game = genre.game;
                        return game;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Game>> GetPublisherGame(string input)
        {
            try
            {
                List<Game> game = null;
                Publisher publisher = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, $"/api/genre/{input}/games/");
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    using (HttpContent content = Response.Content)
                    {
                        publisher = await content.ReadFromJsonAsync<Publisher>();
                        game = publisher.game;
                        return game;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Game>> GetSearchGame(string input)
        {
            try
            {
                List<Game> game = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/games/" + input);
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    using (HttpContent content = Response.Content)
                    {
                        game = await content.ReadFromJsonAsync<List<Game>>();
                        return game;
                    }
                }
                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<List<User>> GetUser(string input)
        {
            try
            {
                List<User> user = null;
                var url = Url.Combine(httpClient.BaseAddress.AbsoluteUri, "/api/users/" + input);
                HttpResponseMessage Response = await httpClient.GetAsync(url);
                if (Response.IsSuccessStatusCode)
                {
                    using (HttpContent content = Response.Content)
                    {
                        user = await content.ReadFromJsonAsync<List<User>>();
                        return user;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
