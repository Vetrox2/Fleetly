using Fleetly.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class VehiclesPanelController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesPanelController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index(string? searchRegistration, string? selectedMake, string? selectedModel, string? sortColumn, bool sortDescending = false)
        {
            var viewModel = await _vehicleService.GetVehicleListAsync(searchRegistration, selectedMake, selectedModel, sortColumn, sortDescending);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            await _vehicleService.CreateAsync(vehicle);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _vehicleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
