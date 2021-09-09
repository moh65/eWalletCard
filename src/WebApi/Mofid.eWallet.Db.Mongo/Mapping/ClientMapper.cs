using Mofid.eWallet.Entities.BusinessObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Mofid.eWallet.Db.Mongo.Mapping
{
	public class ClientMapper : ClassMapperBase
	{
		public override void Map()
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(Client)))
			{
				BsonClassMap.RegisterClassMap<Client>(m =>
				{
					m.AutoMap();
					m.SetIgnoreExtraElements(true);
					m.MapIdMember(f => f.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
					m.IdMemberMap.SetSerializer(new StringSerializer().WithRepresentation(BsonType.ObjectId));
				});
			}
		}
	}
}
