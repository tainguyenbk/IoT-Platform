using IoTPlatform.Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Data
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(IDatabaseSetting connectionString)
        {
            var client = new MongoClient(connectionString.ConnectionString);
            Database = client.GetDatabase(connectionString.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
