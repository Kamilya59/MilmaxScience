using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;

namespace MilmaxScience.Controllers
{
    public class AdminEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(e => e.City).Include(e => e.EventType).Include(e => e.Organiser).Include(e => e.Speaker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Details/5
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

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name");
            ViewData["OrganiserId"] = new SelectList(_context.Organisers, "Id", "Name");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "FullName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ShortDescription,FullDescription,EventDateTime,MaxParticipants,CityId,SpeakerId,EventTypeId,OrganiserId,Address,ImageUrl")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.EventDateTime = DateTime.SpecifyKind(@event.EventDateTime, DateTimeKind.Utc);
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", @event.EventTypeId);
            ViewData["OrganiserId"] = new SelectList(_context.Organisers, "Id", "Name", @event.OrganiserId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "FullName", @event.SpeakerId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", @event.EventTypeId);
            ViewData["OrganiserId"] = new SelectList(_context.Organisers, "Id", "Name", @event.OrganiserId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "FullName", @event.SpeakerId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDescription,FullDescription,EventDateTime,MaxParticipants,CityId,SpeakerId,EventTypeId,OrganiserId,Address,ImageUrl")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", @event.EventTypeId);
            ViewData["OrganiserId"] = new SelectList(_context.Organisers, "Id", "Name", @event.OrganiserId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "FullName", @event.SpeakerId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.City)
                .Include(e => e.EventType)
                .Include(e => e.Organiser)
                .Include(e => e.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}

