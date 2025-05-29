using Biletado.Domain.Building;
using Biletado.Domain.Room;
using Biletado.Domain.Storey;
using Microsoft.EntityFrameworkCore;

namespace Biletado.Adapters.Datenbank
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Building> buildings { get; set; }
        public DbSet<Storey> storeys { get; set; }
        public DbSet<Room> rooms { get; set; }
    }
}
