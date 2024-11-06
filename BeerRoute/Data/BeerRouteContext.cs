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
    }
}
