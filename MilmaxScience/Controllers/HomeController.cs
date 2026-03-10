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

        //public async Task<IActionResult> Index()
        //{
        //    var events = await _context.Events
        //        .Include(e => e.City)
        //        .Include(e => e.Speaker)
        //        .Include(e => e.EventType)
        //        .ToListAsync();

        //    return View(events);
        //}

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
