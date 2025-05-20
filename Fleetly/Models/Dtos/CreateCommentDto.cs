using System.ComponentModel.DataAnnotations;

namespace Fleetly.Models.Dtos
{
    public class CreateCommentDto
    {
        public string Text { get; set; } = string.Empty;
    }

}
