using ParkingAPI.Entities;
using System.Threading.Tasks;
using System;
using ParkingAPI.Models;

namespace ParkingAPI.Services.Interfaces
{
    public interface IParkingService
    {
        Task<bool> CheckinAsync(Guid companyId, Guid vehicleId);
        Task<bool> CheckoutAsync(Guid companyId, Guid vehicleId);
        Task<ParkingModel> FindParkedVehicleAsync(Guid companyId, Guid vehicleId);
    }
}
