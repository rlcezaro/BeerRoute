﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Data;
using BeerRoute.Models;
using BeerRoute.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BeerRoute.Controllers
{
    public class CervejariaTipoCervejasController : Controller
    {
        private readonly BeerRouteContext _context;
        private readonly IConfiguration _configuration;

        public CervejariaTipoCervejasController(BeerRouteContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (_context.CervejariaTipoCerveja == null)
            {
                return Problem("Entity set 'BeerRouteContext.CervejariaTipoCerveja' is null.");
            }

            var cervejariaTipoCervejas = await _context.CervejariaTipoCerveja
                .Include(ctc => ctc.Cervejaria)
                .Include(ctc => ctc.TipoCerveja)
                .ToListAsync();

            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View(cervejariaTipoCervejas);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var cervejariaTipoCervejas = await _context.CervejariaTipoCerveja
                .Include(ctc => ctc.Cervejaria)
                .Include(ctc => ctc.TipoCerveja)
                .ToListAsync();

            return View(cervejariaTipoCervejas);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja
                .Include(ctc => ctc.Cervejaria)
                .ThenInclude(c => c.CervejariaTiposCervejas)
                .ThenInclude(ctc => ctc.TipoCerveja)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejaria = cervejariaTipoCerveja.Cervejaria;
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View(cervejaria);
        }

        [Authorize]
        public IActionResult Create()
        {
            var vm_cervejarias = new ViewModelCervejariaCervejas();
            vm_cervejarias.Cervejarias = _context.Cervejaria?.ToList() ?? new List<Cervejaria>();
            vm_cervejarias.TipoCervejas = _context.TipoCerveja?.ToList() ?? new List<TipoCerveja>();
            return View(vm_cervejarias);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CervejariaId,TipoCervejaId")] CervejariaTipoCerveja cervejariaTipoCerveja)
        {
            _context.Add(cervejariaTipoCerveja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            var cervejariaTipoCerveja = await _context.CervejariaTipoCerveja
                .Include(ctc => ctc.Cervejaria)
                .Include(ctc => ctc.TipoCerveja)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cervejariaTipoCerveja == null)
            {
                return NotFound();
            }

            return View(cervejariaTipoCerveja);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CervejariaTipoCerveja == null)
            {
                return Problem("Entity set 'BeerRouteContext.CervejariaTipoCerveja' is null.");
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

        [Authorize]
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null || _context.Cervejaria == null)
            {
                return NotFound();
            }

            var cervejaria = await _context.Cervejaria
                .Include(c => c.CervejariaTiposCervejas)
                .ThenInclude(ctc => ctc.TipoCerveja)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cervejaria == null)
            {
                return NotFound();
            }

            return View(cervejaria);
        }
    }
}

