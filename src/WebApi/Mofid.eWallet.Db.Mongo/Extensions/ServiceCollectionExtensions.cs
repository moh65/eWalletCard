using Microsoft.Extensions.DependencyInjection;
using Mofid.eWallet.Db.Mongo.Mapping;
using Mofid.eWallet.Db.Mongo.Repositories;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;

namespace Mofid.eWallet.Db.Mongo
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMongoDatabase(this IServiceCollection services)
		{
			services.AddSingleton<IDatabaseContext, DatabaseContext>();
			services.AddScoped<IClientRepository, ClientRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ITemporaryClientRepository, TemporaryClientRepository>();
			services.AddScoped<ITransactionRepository, TransactionRepository>();

			ClassMapperBase.MapAllClasses();

			return services;
		}
	}
}
