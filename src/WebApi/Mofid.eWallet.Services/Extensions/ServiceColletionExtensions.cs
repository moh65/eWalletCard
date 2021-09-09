using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Services.Contracts;
using Mofid.eWallet.Services.Implementations;

namespace Mofid.eWallet.Services.Extensions
{
	public static class ServiceColletionExtensions
	{
		public static IServiceCollection AddCoreServices(this IServiceCollection services)
		{
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IClientKycService, ClientKycService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ICardService, CardService>();
			services.AddScoped<IClientStateService, ClientStateService>();
			services.AddScoped<ITemporaryClientService, TemporaryClientService>();
			services.AddScoped<IPaymentService, PaymentService>();

			return services;
		}
	}
}
