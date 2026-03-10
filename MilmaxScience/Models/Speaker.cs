using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models
{
    public class Speaker
    {

        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        // Внешний ключ
        public int? CityId { get; set; }
        public City? City { get; set; }

        public List<Event>? Events { get; set; }
        public List<SpeakerSpecialization>? Specializations { get; set; }
    }
}
