using Mofid.eWallet.Entities.BusinessObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace Mofid.eWallet.Db.Mongo.Mapping
{
	public class TokenMapper : ClassMapperBase
	{
		public override void Map()
		{
			BsonClassMap.RegisterClassMap<Token>(a =>
			{
				a.AutoMap();
				a.MapMember(m => m.AccessTokenExpire).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
				a.MapMember(m => m.RefreshTokenExpire).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
				a.MapMember(m => m.HandshakeExpireDate).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
			});
		}
	}
}
