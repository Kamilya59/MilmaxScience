using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models
{
    public class SpeakerRequest
    {
        public int Id { get; set; }

        [Required]
        public string Contact { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}