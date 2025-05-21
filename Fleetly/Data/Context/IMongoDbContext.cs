using MongoDB.Driver;

namespace Fleetly.Data.Context
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }
    }
}