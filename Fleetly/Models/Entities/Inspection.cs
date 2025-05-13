using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Fleetly.Models.Entities
{
    public class Inspection
    {
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public DateTime DatePerformed { get; set; }
        public bool IsPassed { get; set; }
        public DateTime ValidUntil { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
