
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkingAPI.Entities;
using ParkingAPI.Models;

namespace ParkingAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<AddressEntity> Address { get; set; }

        public virtual DbSet<CompanyEntity> Company { get; set; }

        public virtual DbSet<VehicleEntity> Vehicle { get; set; }
        public virtual DbSet<ParkingEntiy> Parking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.HasIndex(e => e.Plate)
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
