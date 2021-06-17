using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;
using GameDB.Service.Manager;
using GameDB.Service.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GameDB.Service.DependencyResolution
{
    public static class ServiceRegistry
    {
        public static void ServiceRegistration(this IServiceCollection services, IAppSettings appSettings)
        {
            AspServiceRegistrations(services);            

            AuthenticationMiddleware.AddAuthentication(services, appSettings);

            services.AddScoped<IGameDbApiManager, GameDbApiManager>();
            services.AddScoped<IGameDBSearchManager, GameDBSearchManager>();
        }


        private static void AspServiceRegistrations(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddDataProtection();
            services.AddSession(ops =>
            {
                ops.Cookie.HttpOnly = true;
                ops.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddMvc();
            services.AddCors();
            services.AddAntiforgery();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal());
        }
    }
}
