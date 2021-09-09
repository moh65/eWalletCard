using MongoDB.Driver;

namespace Mofid.eWallet.Db.Mongo
{
	public interface IDatabaseContext
	{
		IMongoCollection<T> GetCollection<T>(string name);
	}
}