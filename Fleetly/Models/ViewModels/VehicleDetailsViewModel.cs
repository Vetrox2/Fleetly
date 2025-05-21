using Fleetly.Models.Entities;

namespace Fleetly.Models.ViewModels
{
    public class VehicleDetailsViewModel
    {
        public Vehicle Vehicle { get; set; } = null!;
        public List<Entities.Route> Routes { get; set; } = new();
        public List<Inspection> Inspections { get; set; } = new();
    }
}
