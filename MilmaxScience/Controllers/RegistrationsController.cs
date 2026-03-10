using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class RegistrationsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationsController(ApplicationDbContext context,
                                   UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    
    // Подтверждение записи
    public async Task<IActionResult> Confirm(int id)
    {
        var ev = await _context.Events.FindAsync(id);
        if (ev == null) return NotFound();

        return View(ev);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmRegistration(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);

        bool alreadyRegistered = _context.Registrations
            .Any(r => r.UserId == user.Id && r.EventId == eventId);

        if (!alreadyRegistered)
        {
            _context.Registrations.Add(new Registration
            {
                UserId = user.Id,
                EventId = eventId
            });

            await _context.SaveChangesAsync();
        }

        return RedirectToAction("My");
    }

    // Мои записи
    public async Task<IActionResult> My()
    {
        var user = await _userManager.GetUserAsync(User);

        var registrations = await _context.Registrations
            .Include(r => r.Event)
            .ThenInclude(e => e.City)
            .Where(r => r.UserId == user.Id)
            .ToListAsync();

        return View(registrations);
    }
}