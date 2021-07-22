using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    public class TruckEntityTypeConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("Truck");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Model)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(e => e.ProductionYear)
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(e => e.ModelYear)
                .HasColumnType("smallint")
                .IsRequired();
        }
    }
}
