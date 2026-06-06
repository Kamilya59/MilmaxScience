using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;
using MilmaxScience.Models.ViewModels;

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

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new EditProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate
            };

            return View(model);
        }

           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (model.BirthDate > DateTime.Today)
            {
                ModelState.AddModelError(
                    "BirthDate",
                    "Дата рождения не может быть в будущем");
            }
            if (model.BirthDate < new DateTime(1900, 1, 1))
            {
                ModelState.AddModelError(
                    "BirthDate",
                    "Введите корректную дату рождения");
            }
            if (model.BirthDate > DateTime.Today.AddYears(-12))
            {
                ModelState.AddModelError(
                    "BirthDate",
                    "Только пользователям старше 12 лет");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;

            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

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