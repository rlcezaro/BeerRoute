using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Data;
using BeerRoute.Models;
using BeerRoute.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace BeerRoute.Controllers
{
    public class VisitasController : Controller
    {
        private readonly BeerRouteContext _context;
        private readonly IConfiguration _configuration;

        public VisitasController(BeerRouteContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Visitas
        public async Task<IActionResult> Index()
        {
            var beerRouteContext = _context.Visita
                .Include(v => v.Usuario)
                .Include(v => v.VisitaCervejarias)
                    .ThenInclude(vc => vc.Cervejaria);
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
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
                .Include(v => v.Usuario)
                .Include(v => v.VisitaCervejarias)
                    .ThenInclude(vc => vc.Cervejaria)
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome");
            ViewData["EstiloCerveja"] = new SelectList(_context.TipoCerveja.Select(tc => tc.Estilo).Distinct());
            ViewData["CervejariaIds"] = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Nome");
            return View();
        }

        // POST: Visitas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,DataVisita,CreditosUtilizados,Avaliacao,Comentario,EstiloCerveja,CervejariaIds")] ViewModelVisita visitaViewModel)
        {
            if (ModelState.IsValid)
            {
                var visita = new Visita
                {
                    UsuarioId = visitaViewModel.UsuarioId,
                    DataVisita = visitaViewModel.DataVisita,
                    CreditosUtilizados = visitaViewModel.CreditosUtilizados,
                    Avaliacao = visitaViewModel.Avaliacao,
                    Comentario = visitaViewModel.Comentario,
                    VisitaCervejarias = visitaViewModel.CervejariaIds.Select(id => new VisitaCervejaria { CervejariaId = id }).ToList()
                };

                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visitaViewModel.UsuarioId);
            ViewData["EstiloCerveja"] = new SelectList(_context.TipoCerveja.Select(tc => tc.Estilo).Distinct(), visitaViewModel.EstiloCerveja);
            ViewData["CervejariaIds"] = new SelectList(_context.CervejariaTipoCerveja.Where(ctc => ctc.TipoCerveja.Estilo == visitaViewModel.EstiloCerveja).Select(ctc => ctc.Cervejaria), "Id", "Nome", visitaViewModel.CervejariaIds);
            return View(visitaViewModel);
        }

        public JsonResult GetCervejariasByEstiloCerveja(string estiloCerveja)
        {
            var cervejarias = _context.CervejariaTipoCerveja
                .Where(ctc => ctc.TipoCerveja.Estilo == estiloCerveja)
                .Select(ctc => new { ctc.Cervejaria.Id, ctc.Cervejaria.Nome })
                .ToList();
            return Json(cervejarias);
        }
        // GET: Visitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.VisitaCervejarias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            var visitaViewModel = new ViewModelVisita
            {
                Id = visita.Id,
                UsuarioId = visita.UsuarioId,
                DataVisita = visita.DataVisita,
                CreditosUtilizados = visita.CreditosUtilizados,
                Avaliacao = visita.Avaliacao,
                Comentario = visita.Comentario,
                CervejariaIds = visita.VisitaCervejarias.Select(vc => vc.CervejariaId).ToList()
            };

            ViewData["CervejariaIds"] = new SelectList(_context.Cervejaria, "Id", "Nome", visitaViewModel.CervejariaIds);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visitaViewModel.UsuarioId);
            return View(visitaViewModel);
        }

        // POST: Visitas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,DataVisita,CreditosUtilizados,Avaliacao,Comentario,CervejariaIds")] ViewModelVisita visitaViewModel)
        {
            if (id != visitaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var visita = await _context.Visita
                        .Include(v => v.VisitaCervejarias)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (visita == null)
                    {
                        return NotFound();
                    }

                    visita.UsuarioId = visitaViewModel.UsuarioId;
                    visita.DataVisita = visitaViewModel.DataVisita;
                    visita.CreditosUtilizados = visitaViewModel.CreditosUtilizados;
                    visita.Avaliacao = visitaViewModel.Avaliacao;
                    visita.Comentario = visitaViewModel.Comentario;

                    // Atualizar as cervejarias da visita
                    visita.VisitaCervejarias.Clear();
                    visita.VisitaCervejarias = visitaViewModel.CervejariaIds.Select(cid => new VisitaCervejaria { VisitaId = visita.Id, CervejariaId = cid }).ToList();

                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(visitaViewModel.Id))
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
            ViewData["CervejariaIds"] = new SelectList(_context.Cervejaria, "Id", "Nome", visitaViewModel.CervejariaIds);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", visitaViewModel.UsuarioId);
            return View(visitaViewModel);
        }

        // GET: Visitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.Usuario)
                .Include(v => v.VisitaCervejarias)
                    .ThenInclude(vc => vc.Cervejaria)
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
                return Problem("Entity set 'BeerRouteContext.Visita' is null.");
            }
            var visita = await _context.Visita
                .Include(v => v.VisitaCervejarias)
                .FirstOrDefaultAsync(m => m.Id == id);
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
