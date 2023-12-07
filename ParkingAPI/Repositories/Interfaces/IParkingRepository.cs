using ParkingAPI.Entities;
using ParkingAPI.Models;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface IParkingRepository : IBaseRepository<ParkingEntiy>
    {
        Task<bool> CheckinAsync(ParkingEntiy entity);
        Task<bool> CheckoutAsync(ParkingEntiy entity);
        Task <ParkingEntiy> FindParkedVehicleAsync(Guid companyId, Guid vehicleId);
    }
}
