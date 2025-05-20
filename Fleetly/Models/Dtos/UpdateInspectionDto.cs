namespace Fleetly.Models.Dtos
{
    public class UpdateInspectionDto
    {
        public DateTime DatePerformed { get; set; }
        public bool IsPassed { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
