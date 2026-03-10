using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        // Навигационное свойство
        public List<Event>? Events { get; set; }
        public List<Speaker>? Speakers { get; set; }
    }
}
