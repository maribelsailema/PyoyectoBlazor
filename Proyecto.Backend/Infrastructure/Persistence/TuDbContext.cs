using Microsoft.EntityFrameworkCore;
using Proyecto.Backend.Domain.Entities.Models;
using Proyecto.Shared.Models;

namespace Proyecto.Backend.Infrastructure.Persistence
{
    public class TuDbContext : DbContext
    {
        public TuDbContext(DbContextOptions<TuDbContext> options) : base(options) { }

        public DbSet<Investigacion> Investigaciones { get; set; }

        // Agrega más DbSet si necesitas más tablas
    }
}
