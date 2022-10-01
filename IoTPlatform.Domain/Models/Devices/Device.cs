using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.Devices
{
    public class Device
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? DeviceID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string DeviceName { get; set; }
        public string DeviceProfileID { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public string? CustomerID { get; set; }
        public bool? Public { get; set; }
        public bool? IsGateway { get; set; }
    }
}
