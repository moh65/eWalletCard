using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Mongo
{
	public class ClientRepository : IClientRepository
	{

		private IMongoCollection<Client> _collection;

		public ClientRepository(IDatabaseContext context)
		{
			_collection = context.GetCollection<Client>(nameof(Client));
		}
		public async Task AddAsync(Client client)
		{
			await _collection.InsertOneAsync(client);
		}

		public async Task<Client> FindByPhoneNumberAsync(string phoneNumber)
		{
			FilterDefinition<Client> phoneFilter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, phoneNumber);
			//var projection = Builders<Client>.Projection.Include(x=>x.Addresses);
			//FindOptions findOptions = new FindOptions();
			var result = await _collection.Find(phoneFilter).FirstOrDefaultAsync();
			return result;
		}
		public async Task<Client> FindByNationalCodeAsync(string nationalCode)
		{
			FilterDefinition<Client> phoneFilter = Builders<Client>.Filter.Eq(x => x.NationalCode, nationalCode);
			// var projection = Builders<Client>.Projection.Include("Token.$");
			// FindOptions findOptions = new FindOptions();
			var result = await _collection.Find(phoneFilter).FirstOrDefaultAsync();
			return result;
		}
		public async Task UpdateClientAsync(Client client)
		{
			var filter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, client.PhoneNumber);

			var update = Builders<Client>.Update
				.Set(t => t.FirstName, client.FirstName)
				.Set(t => t.LastName, client.LastName)
				.Set(t => t.Username, client.Username)
				.Set(t => t.NickName, client.NickName)
				.Set(t => t.FinancialLevel.LevelName, client.FinancialLevel.LevelName)
				.Set(t => t.FinancialLevel.Level, client.FinancialLevel.Level)
				.Set(t => t.FinancialLevel.Value, client.FinancialLevel.Value)
				.Set(t => t.BirthDate, client.BirthDate)
				.Set(t => t.BourseCode, client.BourseCode)
				.Set(t => t.Addresses, client.Addresses)
				//.Set(t => t.CardStatus, client.CardStatus)
				.Set(t => t.MofidCard, client.MofidCard)
				.Set(t => t.UserId, client.UserId);

			await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
		}

		public async Task UpdateClientTokenAsync(Client client, Token token)
		{
			var findFilter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, client.PhoneNumber)
					   & Builders<Client>.Filter.ElemMatch(x => x.Tokens, Builders<Token>.Filter.Eq(x => x.DeviceId, token.DeviceId));
			var item = await _collection.Find(findFilter).FirstOrDefaultAsync();

			if (item != null && item.Tokens.Any(x=>x.DeviceId == token.DeviceId))
			{
				var update = Builders<Client>.Update
					.Set(t => t.Tokens[-1].AccessToken, token.AccessToken)
					.Set(t => t.Tokens[-1].AccessTokenExpire, token.AccessTokenExpire)
					.Set(t => t.Tokens[-1].RefreshToken, token.RefreshToken)
					.Set(t => t.Tokens[-1].RefreshTokenExpire, token.RefreshTokenExpire)
					.Set(t => t.Tokens[-1].KeyId, token.KeyId)
					.Set(t => t.Tokens[-1].HandshakeExpireDate, token.HandshakeExpireDate)
					.Set(t => t.Tokens[-1].TokenAcquire, token.TokenAcquire);

				await _collection.UpdateOneAsync(findFilter, update, new UpdateOptions { IsUpsert = false });
			}
			else
			{
				var insertFilter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, client.PhoneNumber);
				var update = Builders<Client>.Update.AddToSet(t => t.Tokens, token);
				await _collection.UpdateOneAsync(insertFilter, update);
			}
		}

		public async Task UpdateClientTokenDeviceAsync(Client client, Token token)
		{
			FilterDefinition<Client> phoneFilter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, client.PhoneNumber);

			var update = Builders<Client>.Update.Set(t => t.Tokens[-1].DeviceId, token.DeviceId).Set(t => t.Tokens[-1].KeyId, token.KeyId).Set(t => t.Tokens[-1].HandshakeExpireDate, token.HandshakeExpireDate);

			await _collection.FindOneAndUpdateAsync<Client>(c => c.PhoneNumber == client.PhoneNumber && c.Tokens.Any(t => t.DeviceId == token.DeviceId),
				update,
				new FindOneAndUpdateOptions<Client, Client>()
				{
					IsUpsert = true
				});
		}



		//public async Task UpdateClientTokenAsync(Client client, Token token)
		//{
		//    var filter = Builders<Client>.Filter.Eq(x => x.NationalCode, client.NationalCode)
		//    & Builders<Client>.Filter.ElemMatch(x => x.Tokens, Builders<Token>.Filter.Eq(x => x.DeviceId, token.DeviceId));

		//    var update = Builders<Client>.Update
		//        .Set(t => t.Tokens[-1].AccessToken, token.AccessToken)
		//        .Set(t => t.Tokens[-1].AccessTokenExpire, token.AccessTokenExpire)
		//        .Set(t => t.Tokens[-1].RefreshToken, token.RefreshToken)
		//        .Set(t => t.Tokens[-1].RefreshTokenExpire, token.RefreshTokenExpire)
		//        .Set(t => t.Tokens[-1].KeyId, token.KeyId)
		//        .Set(t => t.Tokens[-1].HandshakeExpireDate, token.HandshakeExpireDate)
		//        .Set(t => t.Tokens[-1].TokenAcquire, token.TokenAcquire);


		//    await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
		//}

		//public async Task UpdateClientTokenDeviceAsync(Client client, Token token)
		//{
		//    FilterDefinition<Client> phoneFilter = Builders<Client>.Filter.Eq(x => x.PhoneNumber, client.PhoneNumber);

		//    var update = Builders<Client>.Update.Set(t => t.Tokens[-1].DeviceId, token.DeviceId).Set(t => t.Tokens[-1].KeyId, token.KeyId).Set(t => t.Tokens[-1].HandshakeExpireDate, token.HandshakeExpireDate);

		//    await _collection.FindOneAndUpdateAsync<Client>(c => c.PhoneNumber == client.PhoneNumber && c.Tokens.Any(t => t.DeviceId == token.DeviceId),
		//        update,
		//        new FindOneAndUpdateOptions<Client, Client>()
		//        {
		//            IsUpsert = true
		//        });
		//}

		public async Task InsertToClientStateAsync(string nationalCode, ClientState clientState)
		{
			var filter = Builders<Client>.Filter.Eq(x => x.NationalCode, nationalCode);
			var update = Builders<Client>.Update.Push(x => x.ClientStates, clientState);
			await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
		}

		public async Task<(long, List<Client>)> TakeAsync(int skip, int take, string nationalCode, string mobile)
		{
			var filter = Builders<Client>.Filter.Empty;
			if (!string.IsNullOrEmpty(nationalCode))
				filter = filter & Builders<Client>.Filter.Regex(x => x.NationalCode, $".*{nationalCode}*.");
			if (!string.IsNullOrEmpty(mobile))
				filter = filter & Builders<Client>.Filter.Regex(x => x.PhoneNumber, $".*{mobile}*.");
			var query = _collection.Find(filter);
			return (await query.CountDocumentsAsync(), await _collection.Find(filter).Skip(skip).Limit(take).ToListAsync());
		}
	}
}
