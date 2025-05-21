namespace Fleetly.Models.ViewModels
{
    public class UpdateVehicleDetailsViewModel
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Registration { get; set; } = null!;
        public int Year { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
