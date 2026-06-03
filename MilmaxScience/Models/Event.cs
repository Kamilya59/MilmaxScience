using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string? Address { get; set; }

        [Required]
        public DateTime EventDateTime { get; set; }

        public int? MaxParticipants { get; set; }
        public string? ImageUrl { get; set; }

        // Связи

        [Required]
        public int CityId { get; set; }
        public City? City { get; set; }

        [Required]
        public int SpeakerId { get; set; }
        public Speaker? Speaker { get; set; }

        [Required]
        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }

        public int? OrganiserId { get; set; }
        public Organiser? Organiser { get; set; }

        public List<Registration>? Registrations { get; set; }
    }
}