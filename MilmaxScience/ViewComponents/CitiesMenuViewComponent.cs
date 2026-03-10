using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using System.Threading.Tasks;

namespace MilmaxScience.ViewComponents
{
    public class CitiesMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CitiesMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cities = await _context.Cities.ToListAsync();
            return View(cities);
        }
    }
}