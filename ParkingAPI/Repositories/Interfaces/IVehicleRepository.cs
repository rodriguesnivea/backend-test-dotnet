using ParkingAPI.Entities;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<VehicleEntity> 
    {
        Task<bool> PlateExist(string plate);
        Task<VehicleEntity> GetByPlateAsync(string plate);
    }
}
