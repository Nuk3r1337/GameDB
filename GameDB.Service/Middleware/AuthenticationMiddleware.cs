using GameDB.Domain.DomainClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameDB.Service.Middleware
{
    internal static class AuthenticationMiddleware
    {
        public static void AddAuthentication(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddAuthentication(ops =>
            {
                ops.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                ops.DefaultChallengeScheme = "GameDbAuth";
                ops.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //ops.RequireAuthenticatedSignIn = true;
            })
            .AddJwtBearer(ops =>
            {
                ops.RequireHttpsMetadata = false;
                ops.SaveToken = true;
            })
            .AddCookie(ops =>
            {
                //ops.LoginPath = new PathString("/logon");
                ops.Cookie.SameSite = SameSiteMode.Strict;
                ops.SlidingExpiration = true;
                ops.ExpireTimeSpan = TimeSpan.FromHours(8);
            }).AddOAuth("GameDbAuth", ops => {
                ops.ClientId = "93797d91-4017-4c0b-bb12-714b220382a1";
                ops.ClientSecret = "6gkwYOtTzaitKvYUCBypzBpeBYCE2c0fcRt9h2KY";
                ops.CallbackPath = new PathString("/callback");
                ops.AuthorizationEndpoint = $"{appSettings.ApiUrl}/oauth/authorize";
                ops.TokenEndpoint = $"{appSettings.ApiUrl}/oauth/token";
                ops.SaveTokens = true;
                ops.UserInformationEndpoint = $"{appSettings.ApiUrl}/api/user";

                ops.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                ops.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                ops.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

                ops.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();
                        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                        context.RunClaimActions(json.RootElement);
                    },
                };
            });
        }
    }
}
