using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.PasargadWallet.Services;

namespace Mofid.eWallet.PasargadWallet.Extensions
{
	public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPasargadWalletServices(this IServiceCollection services)
        {
            services.AddScoped<IBankRegistrationExternalServcie, BankRegistrationServcie>();
            services.AddScoped<IBankCustomerRemainBalanceService, BankCustomerRemainBalanceService>();
            services.AddScoped<IBankTokenExternalService, BankTokenService>();
            services.AddScoped<IBankCardService, BankCardService>();


            return services;
        }
    }
}