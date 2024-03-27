using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Entities;
using ParkingAPI.Models;

namespace ParkingAPI.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.HasIndex(e => e.Plate)
                    .IsUnique();
            });

            modelBuilder.Entity<CompanyEntity>(entity =>
            {
                entity.HasIndex(e => e.CNPJ)
                    .IsUnique();
            });

            // Apenas chame base.OnModelCreating uma vez
            //base.OnModelCreating(modelBuilder);
        }
    }
}
