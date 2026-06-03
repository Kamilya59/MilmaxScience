using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;

namespace MilmaxScience.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id, string? source)
        {
            if (id == null)
                return NotFound();

            var @event = await _context.Events
                .Include(e => e.City)
                .Include(e => e.EventType)
                .Include(e => e.Organiser)
                .Include(e => e.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
                return NotFound();

            ViewBag.Source = source;

            return View(@event);
        }

        public async Task<IActionResult> Index(string filter, int? cityId)
        {
            filter ??= "all";
            var events = _context.Events
                .Include(e => e.City)
                .Include(e => e.Speaker)
                .Include(e => e.Registrations)
                .AsQueryable();

            // ФИЛЬТР ПО ДАТЕ
            switch (filter)
            {
                case "past":
                    events = events.Where(e => e.EventDateTime < DateTime.UtcNow);
                    break;

                case "upcoming":
                    events = events.Where(e => e.EventDateTime >= DateTime.UtcNow);
                    break;

                case "all":
                default:
                    break;
            }

            // ФИЛЬТР ПО ГОРОДУ
            if (cityId.HasValue)
            {
                events = events.Where(e => e.CityId == cityId);
            }

            if (cityId.HasValue)
            {
                var city = await _context.Cities.FindAsync(cityId.Value);
                ViewBag.CurrentCityName = city?.Name;
            }
            else
            {
                ViewBag.CurrentCityName = null;
            }

            // чтобы не терялось в View
            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentCity = cityId;

            return View(await events
                .OrderBy(e => e.EventDateTime < DateTime.UtcNow)
                .ThenBy(e => e.EventDateTime)
                .ToListAsync());
        }
    }
}