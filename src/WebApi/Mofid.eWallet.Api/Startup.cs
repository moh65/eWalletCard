using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mofid.eWallet.Api.Extensions;
using Mofid.eWallet.Api.Filters;
using Mofid.eWallet.Api.Middlewares;
using Mofid.eWallet.Api.ServiceExtensions;
using Mofid.eWallet.BO.Tbs.Extensions;
using Mofid.eWallet.Db.Mongo;
using Mofid.eWallet.Domain.Configurations;
using Mofid.eWallet.Entities.Configurations;
using Mofid.eWallet.Infra.ElasticSearch;
using Mofid.eWallet.Infra.Extensions;
using Mofid.eWallet.Infra.Security;
using Mofid.eWallet.Moq;
using Mofid.eWallet.PasargadWallet.Extensions;
using Mofid.eWallet.Services.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Mofid.eWallet.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddControllers();
            services.AddHttpClient();

            services.AddInfraServices();
            services.Configure<BackofficeConfiguration>(Configuration.GetSection("BackOfficConfig"));
            services.Configure<BankConfiguration>(Configuration.GetSection("BankConfig"));
            services.Configure<JWTConfiguration>(Configuration.GetSection("JwtSettings"));
            var data = Environment.GetEnvironmentVariable("MongoDatabaseConnection:ConnectionString");
            var con = Configuration.GetSection("MongoDatabaseConnection").Get<DatabaseSetting>();
            if (!string.IsNullOrEmpty(data))
            {
                con.ConnectionString = data;
            }
            services.Configure<DatabaseSetting>(x =>
            {
                x.ConnectionString = con.ConnectionString;
                x.Database = con.Database;
            });
            services.Configure<ElasticConnectionSettings>(Configuration.GetSection("ElasticConnectionSettings"));
            //services.ConfigureJWT(Configuration);

            //services.AddPasargadWalletServices();
            //services.ConfigureJWT(Configuration);
            //services.AddTbsServiceForBackffice();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine(env);
            if (env != null && env.ToLower().Equals("loadtest"))
            {
                //services.AddPasargadWalletServices();
                services.AddMoqBankServices();
                services.AddMoqTbsServices();

            }
            else
            {
                services.AddPasargadWalletServices();
                services.AddTbsServiceForBackoffice();
            }

            services.AddMongoDatabase();
            //services.AddCommons();


            services.AddCoreServices();
            services.AddSwaggerAndVersioningServices();


            services.AddLogging();
            services.ConfigRateLimit(Configuration);
            services.AddScoped<AuditRequest>();
            services.AddScoped<AuditRequestResponse>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

            }
            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();
            app.UseRouting();
            app.ResponseException();
            app.UseJWT();
            app.UseIpRateLimiting();

            // needed for  ${aspnet-request-posted-body} with an API Controller. Must be before app.UseEndpoints
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => {
                    context.Response.Redirect("/swagger");
                });
            });
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            //app.MapWhen(context => !context.Request.Path.ToString().Contains("."),
            //   appBuilder =>
            //   {
            //	   appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>();
            //   }
            //   );
            app.UseMySwaggerSetting(provider);


        }
    }
}
