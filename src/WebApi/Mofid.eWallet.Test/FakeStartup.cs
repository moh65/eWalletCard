
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Test
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
              
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMiddleware<FakeRemoteIpAddressMiddleware>();

           

            base.Configure(app, env, provider);
        }
    }
}
