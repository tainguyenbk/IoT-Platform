using IoTPlatform.Domain.Repositories;
using IoTPlatform.Infrastructure.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories
{
    public class MongoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected MongoRepository(IOptions<DatabaseSetting> databaseSetting)
        {
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);

            Database = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            DbSet = Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async virtual Task<TEntity> Add(TEntity obj)
        {
            await DbSet.InsertOneAsync(obj);
            return obj;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var data = await DbSet.FindAsync(_ => true);
            return data.ToList();
        }

        public async Task<TEntity> GetById(string id)
        {
            var data = await DbSet.Find(FilterId(id)).SingleOrDefaultAsync();
            return data;
        }

        public async Task<bool> Remove(string id)
        {
            var data = await DbSet.DeleteOneAsync(FilterId(id));
            return data.IsAcknowledged;
        }

        public async Task<TEntity> Update(string id, TEntity obj)
        {
            await DbSet.ReplaceOneAsync(FilterId(id), obj);
            return obj;
        }
        private static FilterDefinition<TEntity> FilterId(string key)
        {
            return Builders<TEntity>.Filter.Eq("_id", new ObjectId(key));
        }
    }
}