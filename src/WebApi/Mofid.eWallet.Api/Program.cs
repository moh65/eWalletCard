using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Mofid.eWallet.Api
{
    [ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}


		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.UseStartup<Startup>()
						.UseIISIntegration()
						.UseContentRoot(Directory.GetCurrentDirectory())
						.UseDefaultServiceProvider(p => p.ValidateScopes = false)
						.ConfigureLogging(logging =>
						{
							//LogManager.Setup()
								   //.LoadConfigurationFromAppSettings();
							logging.ClearProviders();
							logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
						}).UseNLog();
				});
	}
}