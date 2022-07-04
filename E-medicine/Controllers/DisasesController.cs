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
    public class DisasesController : Controller
    {
        private readonly E_medicineContext _context;

        public DisasesController(E_medicineContext context)
        {
            _context = context;
        }

        // GET: Disases
        public async Task<IActionResult> Index()
        {
              return _context.Disase != null ? 
                          View(await _context.Disase.ToListAsync()) :
                          Problem("Entity set 'E_medicineContext.Disase'  is null.");
        }

        // GET: Disases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disase == null)
            {
                return NotFound();
            }

            var disase = await _context.Disase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disase == null)
            {
                return NotFound();
            }

            return View(disase);
        }

        // GET: Disases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Organs")] Disase disase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disase);
        }

        // GET: Disases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disase == null)
            {
                return NotFound();
            }

            var disase = await _context.Disase.FindAsync(id);
            if (disase == null)
            {
                return NotFound();
            }
            return View(disase);
        }

        // POST: Disases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Organs")] Disase disase)
        {
            if (id != disase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisaseExists(disase.Id))
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
            return View(disase);
        }

        // GET: Disases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disase == null)
            {
                return NotFound();
            }

            var disase = await _context.Disase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disase == null)
            {
                return NotFound();
            }

            return View(disase);
        }

        // POST: Disases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disase == null)
            {
                return Problem("Entity set 'E_medicineContext.Disase'  is null.");
            }
            var disase = await _context.Disase.FindAsync(id);
            if (disase != null)
            {
                _context.Disase.Remove(disase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisaseExists(int id)
        {
          return (_context.Disase?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
