using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Mofid.eWallet.Api.ServiceExtensions
{
    [ExcludeFromCodeCoverage]
	public static class JwtTokenExtentions
	{
		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");


			//************************************************************************//
			//var secretKey = Environment.GetEnvironmentVariable("SECRET"); 
			//the beter method to store secret. store in glabal system variables 
			//setx SECRET "MofidWalletSecretKey" /M 
			var secretKey = jwtSettings.GetSection("secret").Value;
			//*************************************************************//

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
					ValidAudience = jwtSettings.GetSection("validAudience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};
			});
		}
		//public static IServiceCollection AddDomainCors(this IServiceCollection services)
		//    => services.AddCors(pulicy=>
		//    {
		//        pulicy.
		//    });

	}
}
