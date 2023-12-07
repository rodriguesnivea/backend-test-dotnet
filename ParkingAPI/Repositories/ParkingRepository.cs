using Microsoft.EntityFrameworkCore;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public class ParkingRepository : BaseRepository<ParkingEntiy>, IParkingRepository
    {
        public ParkingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckinAsync(ParkingEntiy entity)
        {
            entity.IsParked = true;
            await _DbSet.AddAsync(entity);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckoutAsync(ParkingEntiy entity)
        {
            entity.IsParked = false;
            entity.UpdateAt = DateTime.Now;
            _DbSet.Update(entity);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<ParkingEntiy> FindParkedVehicleAsync(Guid companyId, Guid vehicleId)
        {
            var entity = await _DbSet
                .Include(p => p.Company)
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(p => p.CompanyId == companyId && p.VehicleId == vehicleId && p.IsParked);

            return entity;
        }
    }
}
