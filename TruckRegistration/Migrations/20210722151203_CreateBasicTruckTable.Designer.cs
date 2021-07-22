﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TruckRegistration.Infrastructure;

namespace TruckRegistration.Migrations
{
    [DbContext(typeof(TruckContext))]
    [Migration("20210722151203_CreateBasicTruckTable")]
    partial class CreateBasicTruckTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TruckRegistration.Models.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<short>("ModelYear")
                        .HasColumnType("smallint");

                    b.Property<short>("ProductionYear")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Truck");
                });
#pragma warning restore 612, 618
        }
    }
}
