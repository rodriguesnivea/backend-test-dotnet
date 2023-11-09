
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ParkingAPI.Models;

namespace ParkingAPI.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleModel> CreateAsync(VehicleModel model);
        Task<VehicleModel> UpdateAsync(Guid id, VehicleModel model);
        Task DeleteAsync(Guid id);
        Task<VehicleModel> GetAsync(Guid id);
        Task<List<VehicleModel>> GetAllAsync();
        Task<bool> Exist(Guid id);
    }
}
