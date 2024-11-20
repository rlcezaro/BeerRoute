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
    public class VisitasController : Controller
    {
        private readonly BeerRouteContext _context;

        public VisitasController(BeerRouteContext context)
        {
            _context = context;
        }

        // GET: Visitas
        public async Task<IActionResult> Index()
        {
            var beerRouteContext = _context.Visita.Include(v => v.Cervejaria).Include(v => v.Usuario);
            return View(await beerRouteContext.ToListAsync());
        }

        // GET: Visitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.Cervejaria)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visitas/Create
        public IActionResult Create()
        {
            ViewData["CervejariaId"] = new SelectList(_context.Cervejaria, "Id", "Nome");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome");
            return View();
        }

        // POST: Visitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,CervejariaId,DataVisita,CreditosUtilizados,Avaliacao,Comentario")] Visita visita)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CervejariaId"] = new SelectList(_context.Cervejaria, "Id", "Nome", visita.CervejariaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visita.UsuarioId);
            return View(visita);
        }

        // GET: Visitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            ViewData["CervejariaId"] = new SelectList(_context.Cervejaria, "Id", "Nome", visita.CervejariaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visita.UsuarioId);
            return View(visita);
        }

        // POST: Visitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,CervejariaId,DataVisita,CreditosUtilizados,Avaliacao,Comentario")] Visita visita)
        {
            //if (id != visita.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!VisitaExists(visita.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CervejariaId"] = new SelectList(_context.Cervejaria, "Id", "Nome", visita.CervejariaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visita.UsuarioId);
            return View(visita);
        }

        // GET: Visitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.Cervejaria)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visita == null)
            {
                return Problem("Entity set 'BeerRouteContext.Visita'  is null.");
            }
            var visita = await _context.Visita.FindAsync(id);
            if (visita != null)
            {
                _context.Visita.Remove(visita);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
          return (_context.Visita?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
