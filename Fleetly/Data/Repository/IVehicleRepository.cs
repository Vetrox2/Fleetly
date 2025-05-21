using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using System.Linq.Expressions;

namespace Fleetly.Data.Repository
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(string id);
        Task<List<Vehicle>> FilterAsync(Expression<Func<Vehicle, bool>> predicate);
        Task CreateAsync(Vehicle vehicle);
        Task UpdateAsync(string id, UpdateVehicleDetailsDto vehicle);
        Task DeleteAsync(string id);
        Task<List<Vehicle>> GetFilteredAsync(string? registration, string? make, string? model, string? sortColumn, bool sortDescending);
        Task<List<string>> GetAllMakesAsync();
        Task<List<string>> GetAllModelsAsync();
    }
}