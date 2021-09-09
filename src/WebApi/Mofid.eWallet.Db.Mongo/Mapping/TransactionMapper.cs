using Mofid.eWallet.Entities.BusinessObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;


namespace Mofid.eWallet.Db.Mongo.Mapping
{
    public class TransactionMapper : ClassMapperBase
    {
        public override void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
            {
                BsonClassMap.RegisterClassMap<Transaction>(m =>
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
