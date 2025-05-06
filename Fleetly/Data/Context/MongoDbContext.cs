using Fleetly.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fleetly.Data.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            Database = client.GetDatabase(options.Value.DatabaseName);
        }
    }
}
