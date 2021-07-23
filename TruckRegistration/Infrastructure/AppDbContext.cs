using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TruckEntityTypeConfiguration());
        }
    }
}
