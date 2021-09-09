using Microsoft.Extensions.Options;
using Mofid.eWallet.Domain.Configurations;
using MongoDB.Driver;

namespace Mofid.eWallet.Db.Mongo
{
	public class DatabaseContext : IDatabaseContext
	{
		private IMongoDatabase _db { get; set; }
		private MongoClient _mongoClient { get; set; }
		public DatabaseContext(IOptions<DatabaseSetting> config)
		{
			_mongoClient = new MongoClient(config.Value.ConnectionString);
			_db = _mongoClient.GetDatabase(config.Value.Database);
		}

		public IMongoCollection<T> GetCollection<T>(string name)
		{
			return _db.GetCollection<T>(name);
		}
	}
}
