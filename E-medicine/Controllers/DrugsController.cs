using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_medicine.Data;
using E_medicine.Models;

namespace E_medicine.Controllers
{
    public class DrugsController : Controller
    {
        private readonly E_medicineContext _context;

        public DrugsController(E_medicineContext context)
        {
            _context = context;
        }

        // GET: Drugs
        public async Task<IActionResult> Index()
        {
              return _context.Drugs != null ? 
                          View(await _context.Drugs.ToListAsync()) :
                          Problem("Entity set 'E_medicineContext.Drugs'  is null.");
        }
        [HttpGet]
        public async Task<IActionResult> Index(string SearchString)
        {
            IQueryable<Drugs> company = _context.Drugs.AsQueryable().Include(x => x.DisaseDrugs).ThenInclude(y=>y.Disase);

      
            if (!string.IsNullOrEmpty(SearchString))
            {
                company = company.Where(s => s.Name.Contains(SearchString));
            }
            return View(company);
        }

            // GET: Drugs/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugs = await _context.Drugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugs == null)
            {
                return NotFound();
            }

            return View(drugs);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DosageForm,Strenght")] Drugs drugs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugs);
        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugs = await _context.Drugs.FindAsync(id);
            if (drugs == null)
            {
                return NotFound();
            }
            return View(drugs);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DosageForm,Strenght")] Drugs drugs)
        {
            if (id != drugs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugsExists(drugs.Id))
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
            return View(drugs);
        }

        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drugs = await _context.Drugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugs == null)
            {
                return NotFound();
            }

            return View(drugs);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Drugs == null)
            {
                return Problem("Entity set 'E_medicineContext.Drugs'  is null.");
            }
            var drugs = await _context.Drugs.FindAsync(id);
            if (drugs != null)
            {
                _context.Drugs.Remove(drugs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugsExists(int id)
        {
          return (_context.Drugs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
