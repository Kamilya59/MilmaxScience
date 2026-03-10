using Microsoft.EntityFrameworkCore;
using MilmaxScience.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MilmaxScience.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<SpeakerSpecialization> SpeakerSpecializations { get; set; }
    }
}
