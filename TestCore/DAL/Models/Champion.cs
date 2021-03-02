using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestCore.DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Champion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("id")]
        public string ChampionId { get; set; }
        [BsonElement("key")]
        public string ChampionKey { get; set; }
        [BsonElement("name")]
        public string ChampionName { get; set; }
        [BsonElement("title")]
        public string ChampionTitle { get; set; }

    }
}
