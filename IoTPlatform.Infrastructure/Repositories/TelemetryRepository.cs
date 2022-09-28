using IoTPlatform.Domain.Models.Telemetries;
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
    public class TelemetryRepository : MongoRepository<Telemetry>, ITelemetryRepository
    {
        public TelemetryRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<Telemetry>> FindTelemetryByDeviceID(string deviceID)
        {
            var filter = Builders<Telemetry>.Filter.Eq("DeviceID", deviceID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
