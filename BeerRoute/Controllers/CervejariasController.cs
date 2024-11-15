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
    public class CervejariasController : Controller
    {
        private readonly BeerRouteContext _context;
        private readonly IConfiguration _configuration;

        public CervejariasController(BeerRouteContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Cervejarias/List
        public IActionResult List()
        {
            var cervejarias = _context.Cervejaria.ToList();
            return View(cervejarias);
        }

        // GET: Cervejarias
        public async Task<IActionResult> Index()
        {
            if (_context.Cervejaria == null)
            {
                return Problem("Entity set 'BeerRouteContext.Cervejaria' is null.");
            }

            var cervejarias = await _context.Cervejaria
                .Where(c => c.Latitude >= -90 && c.Latitude <= 90 && c.Longitude >= -180 && c.Longitude <= 180)
                .ToListAsync();

            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View(cervejarias);
        }



        // GET: Cervejarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cervejaria == null)
            {
                return NotFound();
            }

            var cervejaria = await _context.Cervejaria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cervejaria == null)
            {
                return NotFound();
            }

            return View(cervejaria);
        }

        // GET: Cervejarias/Create
        public IActionResult Create()
        {
            ViewBag.ApiKey = _configuration["ApiSettings:ApiKey"];
            return View();
        }

        // POST: Cervejarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Latitude,Longitude,Preco,Descricao,Telefone,Email,Site,Facebook,Instagram,ImagemUrl")] Cervejaria cervejaria)
        {
            // Arredondar Latitude e Longitude para 5 casas decimais
            cervejaria.Latitude = Math.Round(cervejaria.Latitude, 5);
            cervejaria.Longitude = Math.Round(cervejaria.Longitude, 5);
            //if (ModelState.IsValid)
            //{
            _context.Add(cervejaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(cervejaria);
        }

        // GET: Cervejarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cervejaria == null)
            {
                return NotFound();
            }

            var cervejaria = await _context.Cervejaria.FindAsync(id);
            if (cervejaria == null)
            {
                return NotFound();
            }
            return View(cervejaria);
        }

        // POST: Cervejarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Latitude,Longitude,Preco,Descricao,Telefone,Email,Site,Facebook,Instagram,ImagemUrl")] Cervejaria cervejaria)
        {
            if (id != cervejaria.Id)
            {
                return NotFound();
            }
            // Arredondar Latitude e Longitude para 5 casas decimais
            cervejaria.Latitude = Math.Round(cervejaria.Latitude, 5);
            cervejaria.Longitude = Math.Round(cervejaria.Longitude, 5);

            _context.Update(cervejaria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(cervejaria);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CervejariaExists(cervejaria.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            ////}           
            ////return View(cervejaria);
        }

        // GET: Cervejarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cervejaria == null)
            {
                return NotFound();
            }

            var cervejaria = await _context.Cervejaria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cervejaria == null)
            {
                return NotFound();
            }

            return View(cervejaria);
        }

        // POST: Cervejarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cervejaria == null)
            {
                return Problem("Entity set 'BeerRouteContext.Cervejaria'  is null.");
            }
            var cervejaria = await _context.Cervejaria.FindAsync(id);
            if (cervejaria != null)
            {
                _context.Cervejaria.Remove(cervejaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CervejariaExists(int id)
        {
          return (_context.Cervejaria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
