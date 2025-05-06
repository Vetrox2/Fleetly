using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Fleetly.Models.Entities
{
    public class Vehicle : IEntity
    {
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }

        public List<Route> Routes { get; set; } = new();

        [BsonIgnore]
        public double TotalDistanceKm => ComputeTotalDistance();

        public List<Inspection> Inspections { get; set; } = new();

        private double ComputeTotalDistance()
        {
            double sum = 0;
            foreach (var route in Routes)
                sum += route.LengthKm;
            return sum;
        }
    }
}
