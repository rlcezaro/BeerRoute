using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Models;

namespace BeerRoute.Data
{
    public class BeerRouteContext : DbContext
    {
        public BeerRouteContext (DbContextOptions<BeerRouteContext> options)
            : base(options)
        {
        }

        public DbSet<BeerRoute.Models.Cervejaria> Cervejaria { get; set; } = default!;

        public DbSet<BeerRoute.Models.TipoCerveja>? TipoCerveja { get; set; }

        public DbSet<BeerRoute.Models.CervejariaTipoCerveja>? CervejariaTipoCerveja { get; set; }

        public DbSet<BeerRoute.Models.Usuario>? Usuario { get; set; }

        public DbSet<BeerRoute.Models.Visita>? Visita { get; set; }

        public DbSet<BeerRoute.Models.Evento>? Evento { get; set; }

        public DbSet<BeerRoute.Models.CompraCredito>? CompraCredito { get; set; }
    }
}
