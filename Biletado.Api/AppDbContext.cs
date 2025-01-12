using Biletado.Api.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Biletado.Api
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Building> buildings { get; set; }
        public DbSet<Storey.Storey> storeys { get; set; }
        public DbSet<Room.Room> rooms { get; set; }
    }
}
