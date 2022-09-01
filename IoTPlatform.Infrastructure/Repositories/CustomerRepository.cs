using IoTPlatform.Domain.Models;
using IoTPlatform.Domain.Repositories;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories
{
    public class CustomerRepository : MongoRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<Customer>> FindCustomerByTitle(string title)
        {
            var filter = Builders<Customer>.Filter.Eq("Title", title);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
