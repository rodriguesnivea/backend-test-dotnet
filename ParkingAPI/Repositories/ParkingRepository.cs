using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Enums;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<int> GetNumberOfParkedVehicles(CompanyEntity company, VehicleEntity vehicle)
        {
            var count = await _DbSet
                .Include(p => p.Vehicle)
                .Where(p => p.Vehicle.typeVehicle == vehicle.typeVehicle && p.CompanyId == company.Id && p.IsParked)
                .CountAsync();
            return count;
        }

        public async Task<int> GetTotalVehicleCheckinsByCompany(Guid companyId)
        {
            var count = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => p.CompanyId == companyId)
               .CountAsync();
            return count;
        }

        public async Task<int> GetTotalVehicleCheckoutsByCompany(Guid companyId)
        {
            var count = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => !p.IsParked && p.CompanyId == companyId)
               .CountAsync();
            return count;
        }

        public async Task<int> GetTotalCarCheckinsByCompanyInTimeRange(Guid companyId, DateTime startTime, DateTime endTime)
        {
            var count = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => p.Vehicle.typeVehicle == TypeVehicle.Car && p.CompanyId == companyId && p.CreateAT >= startTime && p.UpdateAt <= endTime)
               .CountAsync();
            return count;
        }

        public async Task<int> GetTotalMotorcycleCheckinsByCompanyInTimeRange(Guid companyId, DateTime startTime, DateTime endTime)
        {
            var count = await _DbSet
                .Include(p => p.Vehicle)
                .Where(p => p.Vehicle.typeVehicle == TypeVehicle.Motocycle && p.CompanyId == companyId)
                .CountAsync();
            return count;
        }

        public async Task<int> GetTotalCarCheckoutsByCompanyInTimeRange(Guid companyId, DateTime startTime, DateTime endTime)
        {
            var count = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => !p.IsParked && p.Vehicle.typeVehicle == TypeVehicle.Car && p.CompanyId == companyId && p.CreateAT >= startTime && p.UpdateAt <= endTime)
               .CountAsync();
            return count;
        }

        public async Task<int> GetTotalMotorcycleCheckoutsByCompanyInTimeRange(Guid companyId, DateTime startTime, DateTime endTime)
        {
            var count = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => !p.IsParked && p.Vehicle.typeVehicle == TypeVehicle.Car && p.CompanyId == companyId && p.CreateAT >= startTime && p.UpdateAt <= endTime)
               .CountAsync();
            return count;
        }

        public async Task<List<ParkingEntiy>> GetParkingHistoryByCompanyInTimeRange(Guid companyId, DateTime startTime, DateTime endTime)
        {
            var list = await _DbSet
               .Include(p => p.Vehicle)
               .Where(p => p.CompanyId == companyId && p.CreateAT >= startTime && p.UpdateAt <= endTime)
               .ToListAsync();
            return list;
        }
    }
}
