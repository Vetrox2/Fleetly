using Fleetly.Data.Context;
using Fleetly.Models.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Fleetly.Data.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;
        public RouteRepository(IMongoDbContext context)
        {
            _vehicles = context.Database.GetCollection<Vehicle>("Vehicles");
        }

        public async Task<List<Models.Entities.Route>> GetByVehicleIdAsync(string vehicleId)
        {
            var vehicle = await _vehicles.Find(v => v.Id == vehicleId)
                                         .Project(v => v.Routes)
                                         .FirstOrDefaultAsync();
            return vehicle ?? new List<Models.Entities.Route>();
        }

        public async Task AddRouteAsync(string vehicleId, Models.Entities.Route route)
        {
            var update = Builders<Vehicle>.Update.Push(v => v.Routes, route);
            await _vehicles.UpdateOneAsync(v => v.Id == vehicleId, update);
        }

        public async Task UpdateRouteAsync(string vehicleId, string routeId, Models.Entities.Route route)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId),
                Builders<Vehicle>.Filter.ElemMatch(v => v.Routes, r => r.Id == routeId));
            var update = Builders<Vehicle>.Update.Set("Routes.$", route);
            await _vehicles.UpdateOneAsync(filter, update);
        }

        public async Task DeleteRouteAsync(string vehicleId, string routeId)
        {
            var update = Builders<Vehicle>.Update.PullFilter(v => v.Routes, r => r.Id == routeId);
            await _vehicles.UpdateOneAsync(v => v.Id == vehicleId, update);
        }

        public async Task<List<Models.Entities.Route>> FilterRoutesAsync(string vehicleId, Expression<Func<Models.Entities.Route, bool>> predicate)
        {
            var routes = await _vehicles.Find(v => v.Id == vehicleId).Project(v => v.Routes)
                                        .FirstOrDefaultAsync();
            return (routes ?? new List<Models.Entities.Route>()).AsQueryable().Where(predicate.Compile()).ToList();
        }
    }
}

