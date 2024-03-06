using ParkingAPI.Entities;
using ParkingAPI.Enums;
using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface IParkingRepository : IBaseRepository<ParkingEntiy>
    {
        Task<bool> CheckinAsync(ParkingEntiy entity);
        Task<bool> CheckoutAsync(ParkingEntiy entity);
        Task<int> GetNumberOfParkedVehicles(CompanyEntity company, VehicleEntity vehicle);
        Task <ParkingEntiy> FindParkedVehicleAsync(Guid companyId, Guid vehicleId);
        Task<int> GetTotalVehicleCheckinsByCompany(Guid companyId);
        Task<int> GetTotalVehicleCheckoutsByCompany(Guid companyId);
        Task<int> GetTotalCarCheckinsByCompanyInTimeRange(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<int> GetTotalMotorcycleCheckinsByCompanyInTimeRange(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<int> GetTotalCarCheckoutsByCompanyInTimeRange(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<int> GetTotalMotorcycleCheckoutsByCompanyInTimeRange(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<List<ParkingEntiy>> GetParkingHistoryByCompanyInTimeRange(Guid companyId, DateTime StartTime, DateTime EndTime);
    }
}
