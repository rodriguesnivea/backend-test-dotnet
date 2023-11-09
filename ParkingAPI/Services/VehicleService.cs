using ParkingAPI.Entities;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using ParkingAPI.Repositories;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _VehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _VehicleRepository = vehicleRepository;
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel model)
        {
            if(await Exist(model.Id)) return null;

            var entity = VehicleMap.ModelToEntity(model);
            await _VehicleRepository.CreateAsync(entity);
            return model;

        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _VehicleRepository.Exist(id))
            {
                await _VehicleRepository.DeleteAsync(id);
            }
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _VehicleRepository.Exist(id);
        }

        public async Task<List<VehicleModel>> GetAllAsync()
        {

            var entities = await _VehicleRepository.GetAllAsync();
            var models = entities.Select(entity => VehicleMap.EntityToModel(entity)).ToList();
            return models;
        }

        public async Task<VehicleModel> GetAsync(Guid id)
        {
            if (await Exist(id))
            {
                var entity = await _VehicleRepository.GetAsync(id);
                var model = VehicleMap.EntityToModel(entity);

                return model;
            }

            return null;
        }

        public async Task<VehicleModel> UpdateAsync(Guid id, VehicleModel model)
        {
            if (await Exist(id))
            {
                var entity = VehicleMap.ModelToEntity(model);
                await _VehicleRepository.UpdateAsync(entity);
                return model;

            }

            return null;
        }
    }
}
