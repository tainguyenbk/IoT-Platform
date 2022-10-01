using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.Attributes
{
    public class Attribute
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? AttributeID { get; set; }
        public string? DeviceID { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string? Key { get; set; }
        public string? ValueType { get; set; }
        public string? Value { get; set; }
    }
}
