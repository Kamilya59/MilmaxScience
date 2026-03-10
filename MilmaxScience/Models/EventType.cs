using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MilmaxScience.Models
{
    public class EventType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Slug { get; set; }

        public List<Event>? Events { get; set; }
    }
}
