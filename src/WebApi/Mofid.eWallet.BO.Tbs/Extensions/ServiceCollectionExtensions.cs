using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.BO.Tbs.TBSServices;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using System.Diagnostics.CodeAnalysis;

namespace Mofid.eWallet.BO.Tbs.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddTbsServiceForBackoffice(this IServiceCollection services)
		{
			services.AddScoped<ICustomerBackOfficeService, CustomerBackOfficeService>();
			services.AddSingleton<CustomerClubExternalService>();

			return services;
		}

	}
}
