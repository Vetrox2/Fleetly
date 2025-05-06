namespace Fleetly.Models.Entities
{
    public class Comment
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Text { get; set; } = null!;
    }
}
