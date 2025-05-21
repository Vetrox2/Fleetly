using Fleetly.Models.Entities;

namespace Fleetly.Models.ViewModels
{
    public class VehicleListViewModel
    {
        public List<Vehicle> Vehicles { get; set; } = new();

        public string? SearchRegistration { get; set; }

        public string? SelectedMake { get; set; }
        public string? SelectedModel { get; set; }

        public List<string> AllMakes { get; set; } = new();
        public List<string> AllModels { get; set; } = new();

        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }

}
