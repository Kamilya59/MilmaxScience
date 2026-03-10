using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilmaxScience.Data;
using MilmaxScience.Models;

namespace MilmaxScience.Controllers
{
    public class OrganisersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganisersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organisers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organisers.ToListAsync());
        }

        // GET: Organisers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organisers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organiser == null)
            {
                return NotFound();
            }

            return View(organiser);
        }

        // GET: Organisers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organisers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LogoUrl,Description")] Organiser organiser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organiser);
        }

        // GET: Organisers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organisers.FindAsync(id);
            if (organiser == null)
            {
                return NotFound();
            }
            return View(organiser);
        }

        // POST: Organisers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LogoUrl,Description")] Organiser organiser)
        {
            if (id != organiser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganiserExists(organiser.Id))
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
            return View(organiser);
        }

        // GET: Organisers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organisers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organiser == null)
            {
                return NotFound();
            }

            return View(organiser);
        }

        // POST: Organisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organiser = await _context.Organisers.FindAsync(id);
            if (organiser != null)
            {
                _context.Organisers.Remove(organiser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganiserExists(int id)
        {
            return _context.Organisers.Any(e => e.Id == id);
        }
    }
}
