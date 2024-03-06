using Microsoft.OpenApi.Extensions;
using ParkingAPI.DTO;
using ParkingAPI.Entities;
using ParkingAPI.Models;
using System.Drawing;

namespace ParkingAPI.Mappers
{
    public class VehicleMap
    {
        public static VehicleEntity ModelToEntity(VehicleModel model)
        {
            VehicleEntity entity = new VehicleEntity()
            {
                Id = model.Id,
                CreateAT = model.CreateAT,
                UpdateAt = model.UpdateAt,
                Brand = model.Brand,
                Model = model.Model,
                Color = model.Color,
                Plate = model.Plate,
                typeVehicle = model.typeVehicle,
            };

            return entity;
        }

        public static VehicleModel EntityToModel(VehicleEntity entity)
        {
            VehicleModel model = new VehicleModel()
            {
                Id = entity.Id,
                CreateAT = entity.CreateAT,
                UpdateAt = entity.UpdateAt,
                Brand = entity.Brand,
                Model = entity.Model,
                Color = entity.Color,
                Plate = entity.Plate,
                typeVehicle = entity.typeVehicle,
            };

            return model;
        }

        public static VehicleDTO ModelToDto (VehicleModel model)
        {
            VehicleDTO dto = new VehicleDTO()
            {
                Id = model.Id,  
                Color = model.Color,
                Model = model.Model,
                Brand = model.Brand,
                Plate = model.Plate,
                typeVehicle = model.typeVehicle

            };

            return dto;
        }

        public static VehicleModel DtoToModel(VehicleDTO dto)
        {
            VehicleModel model = new VehicleModel()
            {
                Color = dto.Color,
                Model = dto.Model,
                Brand = dto.Brand,
                Plate = dto.Plate,
                typeVehicle = dto.typeVehicle

            };

            return model;
        }

        public static VehicleDTO EntityToDto(VehicleEntity entity)
        {
            VehicleDTO dto = new VehicleDTO()
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Color = entity.Color,
                Plate = entity.Plate,
                typeVehicle = entity.typeVehicle,
            };

            return dto;
        }
    }
}
 