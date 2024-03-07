using ParkingAPI.DTO;
using ParkingAPI.Entities;
using ParkingAPI.Models;

namespace ParkingAPI.Mappers
{
    public static class ParkingMap
    {
        public static ParkingEntiy ModelToEntity(ParkingModel model)
        {
            ParkingEntiy entity = new ParkingEntiy()
            {
                Id = model.Id,
                CreateAT = model.CreateAT,
                UpdateAt = model.UpdateAt,
                CompanyId = model.CompanyId,
                VehicleId = model.VehicleId,
                IsParked = model.IsParked
            };

            return entity;
        }

        public static ParkingModel EntityToModel(ParkingEntiy entity)
        {
            ParkingModel model = new ParkingModel()
            {
                Id = entity.Id,
                CreateAT = entity.CreateAT,
                UpdateAt = entity.UpdateAt,
                CompanyId = entity.CompanyId,
                VehicleId = entity.VehicleId,
                IsParked = entity.IsParked,
                Company = CompanyMap.EntityToModel(entity.Company),
                Vehicle = VehicleMap.EntityToModel(entity.Vehicle)
            };
            
            return model;
        }


        public static ParkingModel DtoToModel(ParkingDto dto)
        {
            ParkingModel model = new ParkingModel()
            {
                
            };
            return model;
        }

        public static ParkingDto ModelToDto(ParkingModel model)
        {
            ParkingDto dto = new ParkingDto()
            {
                
            };
            return dto;
        }
    }
}
