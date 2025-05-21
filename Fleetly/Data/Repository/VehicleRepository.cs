using Fleetly.Data.Context;
using Fleetly.Models.Dtos;
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

        public async Task UpdateAsync(string id, UpdateVehicleDetailsDto dto)
        {
            var update = Builders<Vehicle>.Update
                .Set(v => v.Make, dto.Make)
                .Set(v => v.Model, dto.Model)
                .Set(v => v.Registration, dto.Registration)
                .Set(v => v.Year, dto.Year)
                .Set(v => v.Photo, dto.Photo);

            await _collection.UpdateOneAsync(v => v.Id == id, update);
        }

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(v => v.Id == id);

        public async Task<List<Vehicle>> GetFilteredAsync(string? registration, string? make, string? model, string? sortColumn, bool sortDescending)
        {
            var filterBuilder = Builders<Vehicle>.Filter;
            var filter = FilterDefinition<Vehicle>.Empty;

            if (!string.IsNullOrWhiteSpace(registration))
                filter &= filterBuilder.Regex(v => v.Registration, new MongoDB.Bson.BsonRegularExpression($".*{registration}.*", "i"));

            if (!string.IsNullOrWhiteSpace(make))
                filter &= filterBuilder.Eq(v => v.Make, make);

            if (!string.IsNullOrWhiteSpace(model))
                filter &= filterBuilder.Eq(v => v.Model, model);

            var vehiclesQuery = _collection.Find(filter);

            if (!string.IsNullOrEmpty(sortColumn))
            {
                var sortBuilder = Builders<Vehicle>.Sort;
                var sortDefinition = sortDescending
                    ? sortBuilder.Descending(sortColumn)
                    : sortBuilder.Ascending(sortColumn);

                vehiclesQuery = vehiclesQuery.Sort(sortDefinition);
            }

            return await vehiclesQuery.ToListAsync();
        }

        public async Task<List<string>> GetAllMakesAsync()
        {
            return await _collection.Distinct<string>("Make", FilterDefinition<Vehicle>.Empty).ToListAsync();
        }

        public async Task<List<string>> GetAllModelsAsync()
        {
            return await _collection.Distinct<string>("Model", FilterDefinition<Vehicle>.Empty).ToListAsync();
        }
    }
}
