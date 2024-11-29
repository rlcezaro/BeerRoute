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
        public BeerRouteContext(DbContextOptions<BeerRouteContext> options)
            : base(options)
        {
        }

        public DbSet<Cervejaria> Cervejaria { get; set; } = default!;
        public DbSet<TipoCerveja>? TipoCerveja { get; set; }
        public DbSet<CervejariaTipoCerveja>? CervejariaTipoCerveja { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }
        public DbSet<Visita>? Visita { get; set; }
        public DbSet<Evento>? Evento { get; set; }
        public DbSet<CompraCredito>? CompraCredito { get; set; }
        public DbSet<VisitaCervejaria> VisitaCervejaria { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }
    }
}
