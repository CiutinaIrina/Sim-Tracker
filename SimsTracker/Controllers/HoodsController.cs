using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.DbContext;
using SimsTracker.Data;
using SimsTracker.Models;

namespace SimsTracker.Controllers
{
    public class HoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public object Application { get; private set; }
        public object Session { get; private set; }

        public HoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Population (int id)
        {
            Hood hood = _context.Hood.FirstOrDefault(hood => hood.id == id);
            string hoodName = hood.name;
            List<Sim> sims = _context.Sim.Where(s => s.hoodName == hoodName).ToList();
            return sims.Count();
        }

        // GET: Hoods
        public async Task<IActionResult> Index()
        {
            List<Hood> hoods = _context.Hood.ToList();
            foreach(Hood hood in hoods)
            {
                hood.population = Population(hood.id);
            }
            return View(await _context.Hood.ToListAsync());
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: Hoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hood = await _context.Hood
                .FirstOrDefaultAsync(m => m.id == id);
            if (hood == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Sims", new { hoodName = hood.name });       
        }

        // GET: Hoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,population")] Hood hood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hood);
        }

        // GET: Hoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hood = await _context.Hood.FindAsync(id);
            if (hood == null)
            {
                return NotFound();
            }
            return View(hood);
        }

        // POST: Hoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,population,Sims")] Hood hood)
        {
            if (id != hood.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoodExists(hood.id))
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
            return View(hood);
        }

        // GET: Hoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hood = await _context.Hood
                .FirstOrDefaultAsync(m => m.id == id);
            if (hood == null)
            {
                return NotFound();
            }

            return View(hood);
        }

        // POST: Hoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hood = await _context.Hood.FindAsync(id);
            _context.Hood.Remove(hood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoodExists(int id)
        {
            return _context.Hood.Any(e => e.id == id);
        }
    }
}
