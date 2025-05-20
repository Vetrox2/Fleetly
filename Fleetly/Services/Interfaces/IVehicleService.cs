using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAsync();
    Task<Vehicle> GetByIdAsync(string id);
    Task CreateAsync(Vehicle vehicle);
    Task UpdateAsync(string id, UpdateVehicleDetailsDto vehicle);
    Task DeleteAsync(string id);
}
