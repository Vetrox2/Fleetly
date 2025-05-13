using Fleetly.Data.Context;
using Fleetly.Models.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Fleetly.Data.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _collection;
        public VehicleRepository(IMongoDbContext context)
        {
            _collection = context.Database.GetCollection<Vehicle>("Vehicles");
        }

        public async Task<List<Vehicle>> GetAllAsync() =>
            await _collection.Find(Builders<Vehicle>.Filter.Empty).ToListAsync();

        public async Task<Vehicle?> GetByIdAsync(string id) =>
            await _collection.Find(v => v.Id == id).FirstOrDefaultAsync();

        public async Task<List<Vehicle>> FilterAsync(Expression<Func<Vehicle, bool>> predicate) =>
            await _collection.Find(predicate).ToListAsync();

        public async Task CreateAsync(Vehicle vehicle) =>
            await _collection.InsertOneAsync(vehicle);

        public async Task UpdateAsync(string id, Vehicle vehicle) =>
            await _collection.ReplaceOneAsync(v => v.Id == id, vehicle);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(v => v.Id == id);
    }
}
