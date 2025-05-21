using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using Fleetly.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAsync();
    Task<Vehicle> GetByIdAsync(string id);
    Task CreateAsync(CreateVehicleDto vehicle);
    Task UpdateAsync(string id, UpdateVehicleDetailsViewModel vehicle);
    Task DeleteAsync(string id);
    Task<VehicleListViewModel> GetVehicleListAsync(string? registration, string? make, string? model, string? sortColumn, bool sortDescending);
}
