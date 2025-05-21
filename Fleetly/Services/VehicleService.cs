using Fleetly.Models.Entities;
using Fleetly.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fleetly.Models.Dtos;
using Fleetly.Models.ViewModels;

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

    public async Task UpdateAsync(string id, UpdateVehicleDetailsDto vehicle) =>
        await _repo.UpdateAsync(id, vehicle);

    public async Task DeleteAsync(string id) =>
        await _repo.DeleteAsync(id);

    public async Task<VehicleListViewModel> GetVehicleListAsync(string? registration, string? make, string? model, string? sortColumn, bool sortDescending)
    {
        var vehicles = await _repo.GetFilteredAsync(registration, make, model, sortColumn, sortDescending);
        var makes = await _repo.GetAllMakesAsync();
        var models = await _repo.GetAllModelsAsync();

        return new VehicleListViewModel
        {
            Vehicles = vehicles,
            SearchRegistration = registration,
            SelectedMake = make,
            SelectedModel = model,
            SortColumn = sortColumn,
            SortDescending = sortDescending,
            AllMakes = makes,
            AllModels = models
        };
    }
}
