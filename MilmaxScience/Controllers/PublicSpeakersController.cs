using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;

public class PublicSpeakersController : Controller
{
    private readonly ApplicationDbContext _context;

    public PublicSpeakersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var speakers = await _context.Speakers
            .Include(s => s.City)
            .ToListAsync();

        return View(speakers);
    }
}