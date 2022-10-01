using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLogs
{
    public class AuditLog
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string AuditLogID { get; set; }
        public DateTime TimeStamp { get; set; }
        public EntityType EntityType { get; set; }
        public string EntityTypeName { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public ActionType ActionType { get; set; }
        public string ActionTypeName { get; set; }
        public string Status { get; set; }

        public AuditLog(DateTime timeStamp, EntityType entityType, string entityID, string entityName, string userName, string userID, ActionType actionType)
        {
            TimeStamp = timeStamp;
            EntityType = entityType;
            EntityTypeName = entityType.ToString();
            EntityID = entityID;
            EntityName = entityName;
            UserName = userName;
            UserID = userID;
            ActionType = actionType;
            ActionTypeName = actionType.ToString();
        }
    }
}
