using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AllRegistrations()
    {
        var registrations = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Event)
                .ThenInclude(e => e.City)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View(registrations);
    }

    public async Task<IActionResult> Registrations(int id)
    {
        var registrations = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Event)
            .Where(r => r.EventId == id)
            .ToListAsync();

        var eventItem = registrations.FirstOrDefault()?.Event;

        ViewBag.Count = registrations.Count;
        ViewBag.Max = eventItem?.MaxParticipants ?? 0;

        ViewBag.EventId = id;

        return View(registrations);
    }

    public async Task<IActionResult> SpeakerRequests()
    {
        var requests = await _context.SpeakerRequests
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return View(requests);
    }
}