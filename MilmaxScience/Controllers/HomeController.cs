using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;
using System.Diagnostics;

namespace MilmaxScience.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult OrderSpeaker()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderSpeaker(string contact)
        {
            if (string.IsNullOrWhiteSpace(contact))
            {
                TempData["Error"] = "Введите телефон или e-mail";
                return RedirectToAction(nameof(OrderSpeaker));
            }

            var request = new SpeakerRequest
            {
                Contact = contact,
                CreatedAt = DateTime.UtcNow
            };

            _context.SpeakerRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Спасибо! Мы свяжемся с вами в ближайшее время.";

            return RedirectToAction(nameof(OrderSpeaker));
        }

        public async Task<IActionResult> Index(int? cityId)
        {
            var cities = await _context.Cities.ToListAsync();
            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            var events = _context.Events
                .Include(e => e.City)
                .Include(e => e.Speaker)
                .AsQueryable();

            if (cityId.HasValue)
            {
                events = events.Where(e => e.CityId == cityId.Value);
            }

            return View(await events.ToListAsync());
        }

    }
}
