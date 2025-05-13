using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Fleetly.Models.Entities;
using Fleetly.Models.Settings;

public class VehicleService : IVehicleService
{
    private readonly IMongoCollection<Vehicle> _vehicles;

    public VehicleService(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _vehicles = database.GetCollection<Vehicle>("Vehicles");
    }

    public async Task<List<Vehicle>> GetAsync() => await _vehicles.Find(_ => true).ToListAsync();

    public async Task<Vehicle> GetByIdAsync(string id) =>
        await _vehicles.Find(v => v.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Vehicle vehicle) => await _vehicles.InsertOneAsync(vehicle);

    public async Task UpdateAsync(string id, Vehicle vehicle) =>
        await _vehicles.ReplaceOneAsync(v => v.Id == id, vehicle);

    public async Task DeleteAsync(string id) => await _vehicles.DeleteOneAsync(v => v.Id == id);
}
