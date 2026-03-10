using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MilmaxScience.Models
{
    public class Organiser
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public List<Event>? Events { get; set; }
    }
}
