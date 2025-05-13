using Fleetly.Models.Entities;

namespace Fleetly.Services.Interfaces
{
    public interface IRouteService
    {
        Task<List<Models.Entities.Route>> GetByVehicleIdAsync(string vehicleId);
        Task AddRouteAsync(string vehicleId, Models.Entities.Route route);
        Task DeleteRouteAsync(string vehicleId, string routeId);
        Task AddAddressAsync(string vehicleId, string routeId, Address address);
        Task DeleteAddressAsync(string vehicleId, string routeId, string addressIndex);
    }
}
