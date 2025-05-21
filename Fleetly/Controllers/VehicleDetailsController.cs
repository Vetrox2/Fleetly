using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using Fleetly.Models.ViewModels;
using Fleetly.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Fleetly.Controllers
{
    public class VehicleDetailsController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IRouteService _routeService;
        private readonly IInspectionService _inspectionService;

        public VehicleDetailsController(
            IVehicleService vehicleService,
            IRouteService routeService,
            IInspectionService inspectionService)
        {
            _vehicleService = vehicleService;
            _routeService = routeService;
            _inspectionService = inspectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            var routes = await _routeService.GetByVehicleIdAsync(id);
            var inspections = await _inspectionService.GetByVehicleIdAsync(id);

            var vm = new VehicleDetailsViewModel
            {
                Vehicle = vehicle,
                Routes = routes,
                Inspections = inspections
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle(string id, UpdateVehicleDetailsViewModel vehicle)
        {
            await _vehicleService.UpdateAsync(id, vehicle);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute(string vehicleId, Models.Entities.Route route)
        {
            await _routeService.AddRouteAsync(vehicleId, route);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoute(string vehicleId, string routeId)
        {
            await _routeService.DeleteRouteAsync(vehicleId, routeId);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(string vehicleId, string routeId, Address address)
        {
            await _routeService.AddAddressAsync(vehicleId, routeId, address);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(string vehicleId, string routeId, int index)
        {
            await _routeService.DeleteAddressAsync(vehicleId, routeId, index.ToString());
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> MoveAddress(string vehicleId, string routeId, int index, string direction)
        {
            await _routeService.MoveAddressAsync(vehicleId, routeId, index, direction);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(string vehicleId, string routeId, int index, UpdateAddressDto updatedAddress)
        {
            await _routeService.UpdateAddressAsync(vehicleId, routeId, index, updatedAddress);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> AddInspection(string vehicleId, Inspection inspection)
        {
            await _inspectionService.AddInspectionAsync(vehicleId, inspection);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInspection(string vehicleId, string inspectionId)
        {
            await _inspectionService.DeleteInspectionAsync(vehicleId, inspectionId);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInspection(string vehicleId, string inspectionId, UpdateInspectionDto updatedInspection)
        {
            await _inspectionService.UpdateInspectionAsync(vehicleId, inspectionId, updatedInspection);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string vehicleId, string inspectionId, string text)
        {
            var comment = new Comment
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Text = text,
                Timestamp = DateTime.UtcNow
            };

            await _inspectionService.AddCommentAsync(vehicleId, inspectionId, comment);

            return RedirectToAction("Details", new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(string vehicleId, string inspectionId, string commentId)
        {
            await _inspectionService.DeleteCommentAsync(vehicleId, inspectionId, commentId);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(string vehicleId, string inspectionId, string commentId, UpdateCommentDto updatedComment)
        {
            await _inspectionService.UpdateCommentAsync(vehicleId, inspectionId, commentId, updatedComment);
            return RedirectToAction(nameof(Details), new { id = vehicleId });
        }
    }
}
