using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Moq.Bank;
using Mofid.eWallet.Moq.Tbs;
using Moq;
using System;
using System.Net.Http.Headers;

namespace Mofid.eWallet.Moq
{
	public static class Extensions
    {

       

        public static IServiceCollection AddMoqTbsServices(this IServiceCollection services)
        {
            var moqTbsService = new MoqCustomerBackOfficeService().GenerateMoqClass();
           
            services.AddScoped(typeof(ICustomerBackOfficeService), service => moqTbsService);
            

            return services;
        }

        public static IServiceCollection AddMoqBankServices(this IServiceCollection services)
        {
            var moqObj = new MoqBankTokenExternalService().GenerateMoqClass();

            services.AddScoped(typeof(ICustomerBackOfficeService), service => new MoqBankInvoiceService().GenerateMoqClass());
            services.AddScoped(typeof(IBankOttExternalService), service => new MoqBankOttService().GenerateMoqClass());
            services.AddScoped(typeof(IBankRegistrationExternalServcie), service => new MoqBankRegistrationServcie().GenerateMoqClass());
            services.AddScoped(typeof(IBankCustomerRemainBalanceService), service => new MoqBankCustomerRemainBalanceService().GenerateMoqClass());
            services.AddScoped(typeof(IBankCardService), service => new MoqBankCardIssucanceService().GenerateMoqClass());
            services.AddScoped(typeof(IBankTokenExternalService),
                (service)
                =>
            {
                return moqObj;
            });

            return services;
        }
    }
}
