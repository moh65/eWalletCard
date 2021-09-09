using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mofid.eWallet.Api.Extensions
{
    [ExcludeFromCodeCoverage]
	public static class SwaggerExtension
	{
		public static IApplicationBuilder UseMySwaggerSetting(this IApplicationBuilder app  , IApiVersionDescriptionProvider provider )
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{

				foreach (var item in provider.ApiVersionDescriptions)
				{

					c.SwaggerEndpoint($"/swagger/v{item.ApiVersion.MajorVersion.ToString() + (item.ApiVersion.MinorVersion == 0?  "" : "." + item.ApiVersion.MinorVersion)}/swagger.json", "api " + item.ApiVersion.MajorVersion.ToString() + "." + item.ApiVersion.MinorVersion);
				}

				
				//c.SwaggerEndpoint("/swagger/v2/swagger.json", "api v2");
			});

			return app;
		}
		public static IServiceCollection AddSwaggerAndVersioningServices(this IServiceCollection services)
		{

			
			services.AddVersionedApiExplorer(options => {
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddApiVersioning(x =>
			{
				x.AssumeDefaultVersionWhenUnspecified = true;
				x.DefaultApiVersion = new ApiVersion(1, 0);
			});
			var provider = services.BuildServiceProvider()
				   .GetRequiredService<IApiVersionDescriptionProvider>();

			services.AddSwaggerGen(config =>
			{
				foreach (var item in provider.ApiVersionDescriptions)
				{
					config.SwaggerDoc(item.GroupName, new OpenApiInfo
					{
						Title = "Mofid Wallet API-" + item.GroupName,
						Version = item.ApiVersion.MajorVersion.ToString() + "." + item.ApiVersion.MinorVersion
					});
				}
				
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				config.IncludeXmlComments(xmlPath);

                //config.OperationFilter<RemoveVersionFromParameter>();
                //config.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                //config.DocInclusionPredicate((version, desc) =>
                //{
                //    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                //    var versions = methodInfo.DeclaringType
                //        .GetCustomAttributes(true)
                //        .OfType<ApiVersionAttribute>()
                //        .SelectMany(attr => attr.Versions);

                //    var maps = methodInfo.GetCustomAttributes(true)
                //        .OfType<MapToApiVersionAttribute>()
                //        .SelectMany(attr => attr.Versions)
                //        .ToArray();
                //    return versions.Any(v => $"v{v.ToString()}" == version)
                //           && (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));
                //});
            });

			return services;
		}
	}
	public class RemoveVersionFromParameter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var versionParameter = operation.Parameters.Single(p => p.Name == "version");
			operation.Parameters.Remove(versionParameter);
		}
	}
	public class ReplaceVersionWithExactValueInPath : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var pathItems = swaggerDoc.Paths
				 .ToDictionary(
					 path => path.Key.Replace("v{version}", swaggerDoc.Info.Version),
					 path => path.Value
				 );
			var newPath = new OpenApiPaths();
			foreach (var item in pathItems)
			{
				newPath.Add(item.Key, item.Value);
			}
			swaggerDoc.Paths = newPath;
		}
	}
}
