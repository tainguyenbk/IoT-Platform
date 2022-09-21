using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLog
{
    public class AuditLog
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string AuditLogId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        public string User { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
