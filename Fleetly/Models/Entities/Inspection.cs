namespace Fleetly.Models.Entities
{
    public class Inspection
    {
        public DateTime DatePerformed { get; set; }
        public bool IsPassed { get; set; }
        public DateTime ValidUntil { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
