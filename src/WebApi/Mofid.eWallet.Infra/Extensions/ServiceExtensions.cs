using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Infra.Caches;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.MiddleWares;
using Mofid.eWallet.Infra.Security;
using Mofid.eWallet.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<ICache, InMemoryCache>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddSingleton(typeof(ElasticClientProvider));
            services.AddSingleton(typeof(ElasticLogger<>));
            return services;
        }


        public static IApplicationBuilder ResponseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseException>();
        }

    }
}
