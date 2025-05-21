using Fleetly.Models.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Fleetly.Models.Dtos
{
    public class UpdateVehicleDetailsDto
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Registration { get; set; } = null!;
        public int Year { get; set; }
        public byte[]? Photo { get; set; }
    }
}
