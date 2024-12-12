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
    public class CompraCreditosController : Controller
    {
        private readonly BeerRouteContext _context;

        public CompraCreditosController(BeerRouteContext context)
        {
            _context = context;
        }

        // GET: CompraCreditos
        public async Task<IActionResult> Index()
        {
            var beerRouteContext = _context.CompraCredito.Include(c => c.Usuario);
            return View(await beerRouteContext.ToListAsync());
        }

        // GET: CompraCreditos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompraCredito == null)
            {
                return NotFound();
            }

            var compraCredito = await _context.CompraCredito
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compraCredito == null)
            {
                return NotFound();
            }

            return View(compraCredito);
        }

        // GET: CompraCreditos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioNome"] = new SelectList(_context.Usuario, "Id", "Nome");
            return View();
        }

        // POST: CompraCreditos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Quantidade,DataCompra")] CompraCredito compraCredito)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(compraCredito);
                await _context.SaveChangesAsync();

                // Atualizar os créditos do usuário
                var usuario = await _context.Usuario.FindAsync(compraCredito.UsuarioId);
                if (usuario != null)
                {
                    usuario.Creditos += compraCredito.Quantidade;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            ////}
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", compraCredito.UsuarioId);
            //return View(compraCredito);
        }


        // GET: CompraCreditos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompraCredito == null)
            {
                return NotFound();
            }

            var compraCredito = await _context.CompraCredito.FindAsync(id);
            if (compraCredito == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", compraCredito.UsuarioId);
            return View(compraCredito);
        }

        // POST: CompraCreditos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Quantidade,DataCompra")] CompraCredito compraCredito)
        {
            if (id != compraCredito.Id)
            {
                return NotFound();
            }

            var compraCreditoOriginal = await _context.CompraCredito.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (compraCreditoOriginal == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(compraCredito.UsuarioId);
            if (usuario == null)
            {
                return NotFound();
            }

            // Calcular a diferença de créditos
            var diferencaCreditos = compraCredito.Quantidade - compraCreditoOriginal.Quantidade;

            //if (ModelState.IsValid)
            //{
                _context.Update(compraCredito);
                await _context.SaveChangesAsync();

                // Atualizar os créditos do usuário
                usuario.Creditos += diferencaCreditos;
                _context.Update(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}

            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", compraCredito.UsuarioId);
            //return View(compraCredito);
        }

        // GET: CompraCreditos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompraCredito == null)
            {
                return NotFound();
            }

            var compraCredito = await _context.CompraCredito
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compraCredito == null)
            {
                return NotFound();
            }

            return View(compraCredito);
        }

        // POST: CompraCreditos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompraCredito == null)
            {
                return Problem("Entity set 'BeerRouteContext.CompraCredito' is null.");
            }

            var compraCredito = await _context.CompraCredito
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (compraCredito != null)
            {
                // Deduzir os créditos do usuário
                var usuario = compraCredito.Usuario;
                if (usuario != null)
                {
                    usuario.Creditos -= compraCredito.Quantidade;
                    _context.Update(usuario);
                }

                _context.CompraCredito.Remove(compraCredito);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool CompraCreditoExists(int id)
        {
          return (_context.CompraCredito?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
