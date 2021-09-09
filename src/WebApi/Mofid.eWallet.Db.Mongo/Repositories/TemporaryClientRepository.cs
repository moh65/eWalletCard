using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Mongo.Repositories
{
	public class TemporaryClientRepository : ITemporaryClientRepository
    {
        private IMongoCollection<TemporaryClient> _collection;

        public TemporaryClientRepository(IDatabaseContext context)
        {
            _collection = context.GetCollection<TemporaryClient>(nameof(TemporaryClient));
        }

        public async Task AddAsync(TemporaryClient item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<TemporaryClient> FindByNationCodeAsync(string nationalCode)
        {
            var filter = Builders<TemporaryClient>.Filter.Eq(x => x.NationalCode, nationalCode);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> IsInWhiteList(string nationalCode)
        {
            var filter = Builders<TemporaryClient>.Filter.Eq(x => x.NationalCode, nationalCode);
            var filter2 = Builders<TemporaryClient>.Filter.Eq(x => x.IsLegal, true);
            return await _collection.Find(filter & filter2).AnyAsync();
        }

        public async Task UpdateAsync(TemporaryClient client)
        {
            var filter = Builders<TemporaryClient>.Filter.Eq(x => x.NationalCode, client.NationalCode);
            var def = Builders<TemporaryClient>.Update.Set(x => x.IsLegal, client.IsLegal);
            await _collection.UpdateOneAsync(filter , def ,  new UpdateOptions { IsUpsert = false });
        }

        public async Task AddAsync(List<TemporaryClient> items)
        {
            await _collection.InsertManyAsync(items);
        }

        public async Task<(long, List<TemporaryClient>)> List(int skip, int take, string nationalCode, string mobile)
        {
            var filter = Builders<TemporaryClient>.Filter.Empty;
            if (!string.IsNullOrEmpty(nationalCode))
                filter = filter & Builders<TemporaryClient>.Filter.Regex(x => x.NationalCode, $".*{nationalCode}*.");
            if (!string.IsNullOrEmpty(mobile))
                filter = filter & Builders<TemporaryClient>.Filter.Regex(x => x.Mobile, $".*{mobile}*.");
            var query =  _collection.Find(filter);
            var list = await _collection.Find(filter).Skip(skip).Limit(take).ToListAsync();
             return (await query.CountDocumentsAsync(), list);
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<TemporaryClient>.Filter.Eq(x => x.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
