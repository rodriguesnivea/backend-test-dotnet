using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingService(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<bool> CheckinAsync(Guid companyId, Guid vehicleId)
        {
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);

            if (entity != null)
            {
                //throw new ServiceException(ApplicationError.NOT_FOUND_EXCEPTION);
            }

            entity = new ParkingEntiy(companyId, vehicleId);
            return await _parkingRepository.CheckinAsync(entity); 
            
        }

        public async Task<bool> CheckoutAsync(Guid companyId, Guid vehicleId)
        {
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);

            if (entity == null)
            {
                //lança ex
            }

            return await _parkingRepository.CheckoutAsync(entity);
        }

        public async Task<ParkingModel> FindParkedVehicleAsync(Guid companyId, Guid vehicleId)
        {
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);
            if(entity == null) return null;

            return ParkingMap.EntityToModel(entity);
        }
    }
}
