using Fleetly.Data.Context;
using Fleetly.Models.Entities;
using MongoDB.Driver;

namespace Fleetly.Data.Repository
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;
        public InspectionRepository(IMongoDbContext context)
        {
            _vehicles = context.Database.GetCollection<Vehicle>("Vehicles");
        }

        public async Task<List<Inspection>> GetByVehicleIdAsync(string vehicleId)
        {
            var vehicle = await _vehicles.Find(v => v.Id == vehicleId)
                                         .Project(v => v.Inspections)
                                         .FirstOrDefaultAsync();
            return vehicle ?? new List<Inspection>();
        }

        public async Task<bool> HasValidInspectionAsync(string vehicleId)
        {
            var inspections = await GetByVehicleIdAsync(vehicleId);
            return inspections.Any(i => i.ValidUntil > DateTime.UtcNow);
        }

        public async Task<DateTime?> ValidUntilAsync(string vehicleId)
        {
            var inspections = await GetByVehicleIdAsync(vehicleId);
            var valid = inspections.Where(i => i.ValidUntil > DateTime.UtcNow)
                                   .OrderByDescending(i => i.ValidUntil)
                                   .FirstOrDefault();
            return valid?.ValidUntil;
        }

        public async Task AddInspectionAsync(string vehicleId, Inspection inspection)
        {
            var update = Builders<Vehicle>.Update.Push(v => v.Inspections, inspection);
            await _vehicles.UpdateOneAsync(v => v.Id == vehicleId, update);
        }

        public async Task UpdateInspectionAsync(string vehicleId, string inspectionId, Inspection inspection)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId),
                Builders<Vehicle>.Filter.ElemMatch(v => v.Inspections, i => i.Id == inspectionId));
            var update = Builders<Vehicle>.Update.Set("Inspections.$", inspection);
            await _vehicles.UpdateOneAsync(filter, update);
        }

        public async Task DeleteInspectionAsync(string vehicleId, string inspectionId)
        {
            var update = Builders<Vehicle>.Update.PullFilter(v => v.Inspections, i => i.Id == inspectionId);
            await _vehicles.UpdateOneAsync(v => v.Id == vehicleId, update);
        }

        public async Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment)
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId),
                Builders<Vehicle>.Filter.ElemMatch(v => v.Inspections, i => i.Id == inspectionId));
            var update = Builders<Vehicle>.Update.Push("Inspections.$.Comments", comment);
            await _vehicles.UpdateOneAsync(filter, update);
        }
    }
}
