using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IoTPlatform.Domain.Models
{
    public class DeviceProfile
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? DeviceProfileID { get; set; }
        public DateTime CreatedTime { get; set; }   
        public string? DeviceProfileName { get; set; }
        public string RuleChain { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TransportType { get; set; }
        public bool Default { get; set; }
    }
}
