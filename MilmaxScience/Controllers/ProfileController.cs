using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;

namespace MilmaxScience.Controllers
{
    
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==========================
        // ЛИЧНЫЙ КАБИНЕТ
        // ==========================
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var registrations = await _context.Registrations
                .Include(r => r.Event)
                .Where(r => r.UserId == user.Id)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var model = new ProfileViewModel
            {
                User = user,
                Registrations = registrations
            };

            return View(model);
        }

        // ==========================
        // ОТМЕНА ЗАПИСИ
        // ==========================
        [HttpPost]
        public async Task<IActionResult> CancelRegistration(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

            if (registration == null)
                return NotFound();

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}