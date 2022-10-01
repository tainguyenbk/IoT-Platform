using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.RuleChains
{
    public class RuleChain
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? RuleChainID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? RuleChainName { get; set; }
        public bool DebugMode { get; set; }
        public bool Root { get; set; }
        public string Description { get; set; }
    }
}
