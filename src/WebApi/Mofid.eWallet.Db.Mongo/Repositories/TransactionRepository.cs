using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Mongo.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private IMongoCollection<Transaction> _collection;

        public TransactionRepository(IDatabaseContext context)
        {
            _collection = context.GetCollection<Transaction>(nameof(Transaction));
        }
        public async Task AddAsync(Transaction transaction)
        {
            await _collection.InsertOneAsync(transaction);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            var filter = Builders<Transaction>.Filter.Eq(x => x.Id, transaction.Id);

            var update = Builders<Transaction>.Update
             .Set(t => t.TransactionStatus, transaction.TransactionStatus)
             .Set(t => t.Type, transaction.Type)
             .Set(t => t.Amount, transaction.Amount)
             .Set(t => t.NationalCode, transaction.NationalCode)
             .Set(t => t.Description, transaction.Description)
             .Set(t => t.ModifiedDate, DateTime.Now);

            await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
        }
    }
}
