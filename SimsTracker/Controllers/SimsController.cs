using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimsTracker.Data;
using SimsTracker.Models;

/*Hi mu name iz Ebony dArk'ness dementia Raven Way (geddit, way lik gerald) */

namespace SimsTracker.Controllers
{
    public class SimsController : Controller
    {
        private string origin { get; set; }
        public object ScriptManager { get; private set; }

        private readonly ApplicationDbContext _context;


        public SimsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Sims

        public int GetAgeByLifeStage(string lifeStage)
        {
            switch (lifeStage)
            {
                case "Baby": return 3;
                case "Toddler": return 4;
                case "Child": return 8;
                case "Teen": return 15;
                case "Young Adult": return 24;
                case "Adult": return 29;
                case "Elder": return 25;
                default: return 31;
            }
        }
        public async Task<IActionResult> Index()
        {
            origin = Request.Query["hoodName"];
            return View(await _context.Sim.Where(s => s.hoodName==origin).ToListAsync());
        }
        public async Task<IActionResult> AgeUp(string hood)
        {
            List<Sim> sims = await _context.Sim.Where(s => s.hoodName == hood).ToListAsync();
            foreach(Sim sim in sims)
            {
                if(sim.status == "Alive")
                {
                    sim.age--;
                }
                if(sim.age == 0 && sim.lifeStage == "Baby")
                {
                    sim.age = 4;
                    sim.lifeStage = "Toddler";
                }
                if (sim.age == 0 && sim.lifeStage == "Toddler")
                {
                    sim.age = 8;
                    sim.lifeStage = "Child";
                }
                if (sim.age == 0 && sim.lifeStage == "Child")
                {
                    sim.age = 15;
                    sim.lifeStage = "Teen";
                    sim.aspiration = "______";
                }
                if (sim.age == 0 && sim.lifeStage == "Teen")
                {
                    sim.age = 24;
                    sim.lifeStage = "Young Adult";
                }
                if (sim.age == 0 && sim.lifeStage == "Young Adult")
                {
                    sim.age = 29;
                    sim.lifeStage = "Adult";
                }
                if (sim.age == 0 && sim.lifeStage == "Adult")
                {
                    sim.age = 31;
                    sim.lifeStage = "Elder";
                }
                if (sim.age == 0 && sim.lifeStage == "Elder")
                {
                    sim.age = 0;
                    sim.status = "Deceased";
                }
                _context.Update(sim);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Sims", new { hoodName = hood });
        }
        public async Task<IActionResult> ExitCollege(int? id)
        {
            var sim = _context.Sim.FirstOrDefault(m => m.id == id);
            if (sim.lifeStage == "Young Adult")
            {
                sim.lifeStage = "Adult";
                sim.age = 29;
            }
            _context.Update(sim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });

        }
        public async Task<IActionResult> SendToCollege(int? id)
        {
            var sim = _context.Sim.FirstOrDefault(m => m.id == id);
            if (sim.lifeStage == "Teen")
            {
                sim.lifeStage = "Young Adult";
                sim.age = 24;
            }
            _context.Update(sim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
        }
        public async Task<IActionResult> Kill(int? id)
        {
            var sim = _context.Sim.FirstOrDefault(m => m.id == id);
            if (sim.status == "Alive")
            {
                sim.status = "Deceased";
                sim.age = GetAgeByLifeStage(sim.lifeStage);
            }
            _context.Update(sim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
        }
        public async Task<IActionResult> Resurrect(int? id)
        {
            var sim = _context.Sim.FirstOrDefault(m => m.id == id);
            if (sim.status == "Deceased")
            {
                sim.status = "Alive";
                sim.age = GetAgeByLifeStage(sim.lifeStage);
            }
            _context.Update(sim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResults(string firstName, string lastName, string lifeStage, string aspiration, string gender,
                                                           string status)
        {
             var result = await _context.Sim
                                .Where(s => (s.firstName.Contains(firstName) || firstName == null)
                                         && (s.lastName.Contains(lastName) || lastName == null)
                                         && (s.lifeStage == lifeStage || lifeStage == null)
                                         && (s.aspiration == aspiration || aspiration == null)
                                         && (s.gender == gender || gender == null)
                                         && (s.status == status || status == null))
                                .ToListAsync();
            return View("Index", result);
        }
        public async Task<IActionResult>/*string*/ Filter(string lifeStage, string aspiration, string gender, string status)
        {
            //return aspiration;
            var result = await _context.Sim
                               .Where(s => (s.lifeStage == lifeStage || lifeStage == null)
                                        && (s.aspiration == aspiration || aspiration == null)
                                        && (s.gender == gender || gender == null)
                                        && (s.status == status || status == null))
                               .ToListAsync();
            return View("Index", result);
        }
        public async Task<IActionResult> Reset()
        {
            var result = await _context.Sim.ToListAsync();
            return View("Index", result);
        }
        // GET: Sims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sim = await _context.Sim
                .FirstOrDefaultAsync(m => m.id == id);
            if (sim == null)
            {
                return NotFound();
            }

            return View(sim);
        }

        // GET: Sims/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Sims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,firstName,lastName,age,lifeStage,gender,aspiration,hoodName,status,notes")] Sim sim)
        {
            //if(sim.lifeStage == "Adult" && sim.aspiration == "Grow Up")
            //{

            //}
            string hood = Request.Query["hoodName"];
            if (ModelState.IsValid)
            {
                _context.Add(sim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
            }
            return View(sim);
        }

        private void alert(string results)
        {
            throw new NotImplementedException(results);
        }

        // GET: Sims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sim = await _context.Sim.FindAsync(id);
            if (sim == null)
            {
                return NotFound();
            }
            return View(sim);
        }

        // POST: Sims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,firstName,lastName,age,lifeStage,gender,aspiration,hoodName,status,notes")] Sim sim)
        {
            if (id != sim.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SimExists(sim.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
            }
            return View(sim);
        }

        // GET: Sims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sim = await _context.Sim
                .FirstOrDefaultAsync(m => m.id == id);
            if (sim == null)
            {
                return NotFound();
            }

            return View(sim);
        }

        // POST: Sims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sim = await _context.Sim.FindAsync(id);
            _context.Sim.Remove(sim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Sims", new { hoodName = sim.hoodName });
        }

        private bool SimExists(int id)
        {
            return _context.Sim.Any(e => e.id == id);
        }
    }
}
