using MongoDB.Bson.Serialization.Attributes;

namespace Fleetly.Models.Entities
{
    public interface IEntity
    {
        [BsonId]
        string Id { get; set; }
    }
}
