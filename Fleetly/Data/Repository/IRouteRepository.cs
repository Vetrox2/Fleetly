using System.Linq.Expressions;

namespace Fleetly.Data.Repository
{
    public interface IRouteRepository
    {
        Task<List<Models.Entities.Route>> GetByVehicleIdAsync(string vehicleId);
        Task AddRouteAsync(string vehicleId, Models.Entities.Route route);
        Task UpdateRouteAsync(string vehicleId, string routeId, Models.Entities.Route route);
        Task DeleteRouteAsync(string vehicleId, string routeId);
        Task<List<Models.Entities.Route>> FilterRoutesAsync(string vehicleId, Expression<Func<Models.Entities.Route, bool>> predicate);
    }
}