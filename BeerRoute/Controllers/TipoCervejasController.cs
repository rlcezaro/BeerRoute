using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Data;
using BeerRoute.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeerRoute.Controllers
{
    [Authorize]
    public class TipoCervejasController : Controller
    {
        private readonly BeerRouteContext _context;

        public TipoCervejasController(BeerRouteContext context)
        {
            _context = context;
        }

        // GET: TipoCervejas
        public async Task<IActionResult> Index()
        {
              return _context.TipoCerveja != null ? 
                          View(await _context.TipoCerveja.ToListAsync()) :
                          Problem("Entity set 'BeerRouteContext.TipoCerveja'  is null.");
        }

        // GET: TipoCervejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoCerveja == null)
            {
                return NotFound();
            }

            var tipoCerveja = await _context.TipoCerveja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCerveja == null)
            {
                return NotFound();
            }

            return View(tipoCerveja);
        }

        // GET: TipoCervejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCervejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Estilo,Pais,Fabricante,IBU,ABV,Descricao,ImagemUrl")] TipoCerveja tipoCerveja)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(tipoCerveja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(tipoCerveja);
        }

        // GET: TipoCervejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoCerveja == null)
            {
                return NotFound();
            }

            var tipoCerveja = await _context.TipoCerveja.FindAsync(id);
            if (tipoCerveja == null)
            {
                return NotFound();
            }
            return View(tipoCerveja);
        }

        // POST: TipoCervejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Estilo,Pais,Fabricante,IBU,ABV,Descricao,ImagemUrl")] TipoCerveja tipoCerveja)
        {
            if (id != tipoCerveja.Id)
            {
                return NotFound();
            }

            _context.Update(tipoCerveja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(tipoCerveja);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TipoCervejaExists(tipoCerveja.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(tipoCerveja);
        }

        // GET: TipoCervejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoCerveja == null)
            {
                return NotFound();
            }

            var tipoCerveja = await _context.TipoCerveja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCerveja == null)
            {
                return NotFound();
            }

            return View(tipoCerveja);
        }

        // POST: TipoCervejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoCerveja == null)
            {
                return Problem("Entity set 'BeerRouteContext.TipoCerveja'  is null.");
            }
            var tipoCerveja = await _context.TipoCerveja.FindAsync(id);
            if (tipoCerveja != null)
            {
                _context.TipoCerveja.Remove(tipoCerveja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCervejaExists(int id)
        {
          return (_context.TipoCerveja?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
