using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    public class TruckContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        public TruckContext(DbContextOptions<TruckContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TruckEntityTypeConfiguration());
        }
    }
}
