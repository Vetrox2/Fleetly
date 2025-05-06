using MongoDB.Bson.Serialization.Attributes;
using System.Net;

namespace Fleetly.Models.Entities
{
    public class Route
    {
        public List<Address> Addresses { get; set; } = new();

        // Computed based on Addresses order
        [BsonIgnore]
        public double LengthKm
        {
            get
            {
                // Place your distance calculation logic here
                return CalculateDistanceFromAddresses(Addresses);
            }
        }

        private double CalculateDistanceFromAddresses(List<Address> addrs)
        {
            // Example stub: real implementation would compute geodesic distances
            return 0.0;
        }
    }
}
