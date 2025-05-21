using Fleetly.Data.Repository;
using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using Fleetly.Services.Interfaces;

namespace Fleetly.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepo;

        public RouteService(IRouteRepository routeRepo)
        {
            _routeRepo = routeRepo;
        }

        public async Task<List<Models.Entities.Route>> GetByVehicleIdAsync(string vehicleId)
        {
            return await _routeRepo.GetByVehicleIdAsync(vehicleId);
        }

        public async Task AddRouteAsync(string vehicleId, Models.Entities.Route route)
        {
            await _routeRepo.AddRouteAsync(vehicleId, route);
        }

        public async Task DeleteRouteAsync(string vehicleId, string routeId)
        {
            await _routeRepo.DeleteRouteAsync(vehicleId, routeId);
        }

        public async Task AddAddressAsync(string vehicleId, string routeId, Address address)
        {
            // Fetch existing route
            var routes = await _routeRepo.GetByVehicleIdAsync(vehicleId);
            var route = routes.Find(r => r.Id == routeId);
            if (route == null) throw new KeyNotFoundException("Route not found");

            route.Addresses.Add(address);
            await _routeRepo.UpdateRouteAsync(vehicleId, routeId, route);
        }

        public async Task DeleteAddressAsync(string vehicleId, string routeId, string addressIndex)
        {
            var routes = await _routeRepo.GetByVehicleIdAsync(vehicleId);
            var route = routes.Find(r => r.Id == routeId);
            if (route == null) throw new KeyNotFoundException("Route not found");

            if (int.TryParse(addressIndex, out int idx) && idx >= 0 && idx < route.Addresses.Count)
            {
                route.Addresses.RemoveAt(idx);
                await _routeRepo.UpdateRouteAsync(vehicleId, routeId, route);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(addressIndex), "Invalid address index");
            }
        }

        public async Task MoveAddressAsync(string vehicleId, string routeId, int index, string direction)
        {
            var routes = await _routeRepo.GetByVehicleIdAsync(vehicleId);
            var route = routes.Find(r => r.Id == routeId);
            if (route == null) throw new KeyNotFoundException("Route not found");

            var addresses = route.Addresses;
            if (index < 0 || index >= addresses.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");

            int newIndex = direction.ToLower() switch
            {
                "up" when index > 0 => index - 1,
                "down" when index < addresses.Count - 1 => index + 1,
                _ => -1
            };

            if (newIndex == -1) return;

            (addresses[index], addresses[newIndex]) = (addresses[newIndex], addresses[index]);
            await _routeRepo.UpdateRouteAsync(vehicleId, routeId, route);
        }

        public async Task UpdateAddressAsync(string vehicleId, string routeId, int index, UpdateAddressDto dto)
        {
            var routes = await _routeRepo.GetByVehicleIdAsync(vehicleId);
            var route = routes.Find(r => r.Id == routeId);
            if (route == null) throw new KeyNotFoundException("Route not found");

            if (index < 0 || index >= route.Addresses.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");

            route.Addresses[index] = new Address
            {
                Street = dto.Street,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            };

            await _routeRepo.UpdateRouteAsync(vehicleId, routeId, route);
        }

    }
}
