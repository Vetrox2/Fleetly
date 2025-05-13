using Fleetly.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class VehiclesPanelController : Controller
    {
        private readonly VehicleService _vehicleService;

        public VehiclesPanelController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAsync();
            return View(vehicles);
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

        public IActionResult Details(string id)
        {
            // To ma być do osobnego kontrolera
            return RedirectToAction("Details", "VehicleDetails", new { id });
        }
    }
}
