using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDB.Domain.DomainClasses;
using GameDB.Service.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GameDB.Service.DependencyResolution
{
    public static class ServiceRegistry
    {
        public static void ServiceRegistration(this IServiceCollection services, IAppSettings appSettings)
        {
            AspServiceRegistrations(services);
            services.AddHttpContextAccessor();

            AuthenticationMiddleware.AddAuthentication(services, appSettings);

            services.AddScoped<IGameDbApiManager, GameDbApiManager>();
        }


        private static void AspServiceRegistrations(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddDataProtection();
            services.AddSession(ops =>
            {
                ops.Cookie.HttpOnly = true;
                ops.Cookie.SameSite = SameSiteMode.None;
            });
            
            services.AddRazorPages();
            services.AddMvcCore();
            services.AddCors();
        }
    }
}
