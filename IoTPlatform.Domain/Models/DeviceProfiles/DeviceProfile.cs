using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.DeviceProfiles
{
    public class DeviceProfile
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? DeviceProfileID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string DeviceProfileName { get; set; }
        public string? RuleChainID { get; set; }
        public List<DeviceProfileImage>? Images { get; set; } = new List<DeviceProfileImage>();
        public string? Description { get; set; }
        public string? TransportType { get; set; }
        [BsonIgnoreIfDefault]
        public bool? Default { get; set; }
    }
}
