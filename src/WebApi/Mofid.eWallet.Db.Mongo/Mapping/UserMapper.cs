using Mofid.eWallet.Entities.BusinessObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Db.Mongo.Mapping
{
    public class UserMapper : ClassMapperBase
    {
        public override void Map()
        {
            BsonClassMap.RegisterClassMap<User>(u =>
            {
                u.AutoMap();
                u.MapIdMember(f => f.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                u.IdMemberMap.SetSerializer(new StringSerializer().WithRepresentation(MongoDB.Bson.BsonType.ObjectId));
            });
        }
    }
}