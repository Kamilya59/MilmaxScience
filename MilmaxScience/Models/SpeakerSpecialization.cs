using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models
{
    public class SpeakerSpecialization
    {
        public int Id { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}