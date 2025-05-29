using Biletado.Api.Domain.Building;
using Microsoft.EntityFrameworkCore;

namespace Biletado.Api.Adapters.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Building> buildings { get; set; }
        public DbSet<Storey.Storey> storeys { get; set; }
        public DbSet<Room.Room> rooms { get; set; }
    }
}
