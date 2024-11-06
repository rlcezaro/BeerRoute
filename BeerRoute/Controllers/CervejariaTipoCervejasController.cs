using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Data;
using BeerRoute.Models;

namespace BeerRoute.Controllers
{
    public class CervejariaTipoCervejasController : Controller
    {
        private readonly BeerRouteContext _context;

        public CervejariaTipoCervejasController(BeerRouteContext context)
        {
            _context = context;
        }

        // GET: CervejariaTipoCervejas
        public async Task<IActionResult> Index()
        {
              return _context.CervejariaTipoCerveja != null ? 
                          View(await _context.CervejariaTipoCerveja.ToListAsync()) :
                          Problem("Entity set 'BeerRouteContext.CervejariaTipoCerveja'  is null.");
        }

        // GET: CervejariaTipoCervejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            return View(cervejariaTipoCerveja);
        }

        // GET: CervejariaTipoCervejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CervejariaTipoCervejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CervejariaId,TipoCervejaId")] CervejariaTipoCerveja cervejariaTipoCerveja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cervejariaTipoCerveja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cervejariaTipoCerveja);
        }

        // GET: CervejariaTipoCervejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja.FindAsync(id);
            if (cervejariaTipoCerveja == null)
            {
                return NotFound();
            }
            return View(cervejariaTipoCerveja);
        }

        // POST: CervejariaTipoCervejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CervejariaId,TipoCervejaId")] CervejariaTipoCerveja cervejariaTipoCerveja)
        {
            if (id != cervejariaTipoCerveja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cervejariaTipoCerveja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CervejariaTipoCervejaExists(cervejariaTipoCerveja.Id))
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
            return View(cervejariaTipoCerveja);
        }

        // GET: CervejariaTipoCervejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            return View(cervejariaTipoCerveja);
        }

        // POST: CervejariaTipoCervejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CervejariaTipoCerveja == null)
            {
                return Problem("Entity set 'BeerRouteContext.CervejariaTipoCerveja'  is null.");
            }
            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja.FindAsync(id);
            if (cervejariaTipoCerveja != null)
            {
                _context.CervejariaTipoCerveja.Remove(cervejariaTipoCerveja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CervejariaTipoCervejaExists(int id)
        {
          return (_context.CervejariaTipoCerveja?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
