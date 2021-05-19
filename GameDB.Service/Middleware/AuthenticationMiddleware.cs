using GameDB.Domain.DomainClasses;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                ops.RequireAuthenticatedSignIn = true;
            })
            .AddJwtBearer(ops =>
            {
                ops.RequireHttpsMetadata = false;
                ops.SaveToken = true;
            })
            .AddCookie(ops =>
            {
                ops.LoginPath = new PathString("/unauthorized");
                ops.Cookie.SameSite = SameSiteMode.Strict;
                ops.SlidingExpiration = true;
                ops.ExpireTimeSpan = TimeSpan.FromHours(8);
            }).AddOAuth("gamdb-oauth", ops => {

            });
        }
    }
}
