using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fleetly.Models.Entities
{
    public class Comment
    {
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Text { get; set; } = null!;
    }
}
