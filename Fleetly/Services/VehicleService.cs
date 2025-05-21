using Fleetly.Models.Entities;
using Fleetly.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fleetly.Models.Dtos;
using Fleetly.Models.ViewModels;
using Microsoft.AspNetCore.Http;

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

    public async Task CreateAsync(CreateVehicleDto model)
    {
        var photoBytes = await MapPhotoToBytes(model.Photo);
        var vehicle = new Vehicle
        {
            Make = model.Make,
            Model = model.Model,
            Registration = model.Registration,
            Year = model.Year,
            Photo = photoBytes
        };
        await _repo.CreateAsync(vehicle);
    }

    public async Task UpdateAsync(string id, UpdateVehicleDetailsViewModel model)
    {
        var photoBytes = await MapPhotoToBytes(model.Photo);
        var vehicle = new UpdateVehicleDetailsDto
        {
            Make = model.Make,
            Model = model.Model,
            Registration = model.Registration,
            Year = model.Year,
            Photo = photoBytes
        };
        await _repo.UpdateAsync(id, vehicle);
    }

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

    private async Task<byte[]?> MapPhotoToBytes(IFormFile? photo)
    {
        byte[]? photoBytes = null;

        if (photo != null && photo.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            photoBytes = memoryStream.ToArray();
        }
        
        return photoBytes;
    }
}
