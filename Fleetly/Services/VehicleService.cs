using Fleetly.Models.Entities;
using Fleetly.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repo;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _repo = vehicleRepository;
    }

    public async Task<List<Vehicle>> GetAsync() =>
        await _repo.GetAllAsync();

    public async Task<Vehicle> GetByIdAsync(string id) =>
        await _repo.GetByIdAsync(id);

    public async Task CreateAsync(Vehicle vehicle) =>
        await _repo.CreateAsync(vehicle);

    public async Task UpdateAsync(string id, Vehicle vehicle) =>
        await _repo.UpdateAsync(id, vehicle);

    public async Task DeleteAsync(string id) =>
        await _repo.DeleteAsync(id);
}
