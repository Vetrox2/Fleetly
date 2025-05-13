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
        Task UpdateAsync(string id, Vehicle vehicle);
        Task DeleteAsync(string id);
    }
}