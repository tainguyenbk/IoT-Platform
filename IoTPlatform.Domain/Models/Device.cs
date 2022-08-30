using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models
{
    public class Device
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? DeviceID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Name { get; set; }
        public string? DeviceProfile { get;set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public bool Public { get; set; }
        public bool IsGateway { get; set; }
    }
}
