using ParkingAPI.DTO;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using System;
using System.IO;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ParkingAPI.Mappers
{
    public static class CompanyMap
    {
        public static CompanyEntity ModelToEntity(CompanyModel companyModel)
        {
            CompanyEntity entity = new CompanyEntity()
            {
                Id = companyModel.Id,
                CreateAT = companyModel.CreateAT,
                UpdateAt = companyModel.UpdateAt,
                Name = companyModel.Name,
                CNPJ = companyModel.CNPJ,
                NumberMotorcycies = companyModel.NumberMotorcycies,
                NumberCars = companyModel.NumberCars,
                Phone = companyModel.Phone,
                Address = AddressModelToAddressEntity(companyModel.Address)
            };
            return entity;
        }

        public static CompanyModel EntityToModel(CompanyEntity companyEntity)
        {
            CompanyModel model = new CompanyModel()
            {
                Id = companyEntity.Id,
                CreateAT = companyEntity.CreateAT,
                UpdateAt = companyEntity.UpdateAt,
                Name = companyEntity.Name,
                CNPJ = companyEntity.CNPJ,
                NumberMotorcycies = companyEntity.NumberMotorcycies,
                NumberCars = companyEntity.NumberCars,
                Phone = companyEntity.Phone,
                Address = AddressEntityToAddressModel(companyEntity.Address)
            };
            AddressEntityToAddressModel(companyEntity.Address);
            return model;
        }

        public static CompanyModel DtoToModel(CompanyDto CompanyDto)
        {
            CompanyModel model = new CompanyModel()
            {
                Id = CompanyDto.Id,
                Name = CompanyDto.Name,
                CNPJ = CompanyDto.CNPJ,
                NumberMotorcycies = CompanyDto.NumberMotorcycies,
                NumberCars = CompanyDto.NumberCars,
                Phone = CompanyDto.Phone,
                Address = AddressDtoToAddressModel(CompanyDto.Address)
            };
            return model;
        }

        public static CompanyDto ModelToDto(CompanyModel companyModel)
        {
            CompanyDto dto = new CompanyDto()
            {
                Id = companyModel.Id,
                Name = companyModel.Name,
                CNPJ = companyModel.CNPJ,
                NumberMotorcycies = companyModel.NumberMotorcycies,
                NumberCars = companyModel.NumberCars,
                Phone = companyModel.Phone,
                Address = AddressModelToDto(companyModel.Address)
            };
            return dto;
        }

        private static AddressEntity AddressModelToAddressEntity(AddressModel addressModel)
        {
            AddressEntity entity = new AddressEntity()
            {
                Id = addressModel.Id,
                CreateAT = addressModel.CreateAT,
                UpdateAt = addressModel.UpdateAt,
                Street = addressModel.Street,
                City = addressModel.City,
                State = addressModel.State,
                Country = addressModel.Country,
                Number = addressModel.Number,
            };

            return entity;
        }

        private static AddressModel AddressEntityToAddressModel(AddressEntity addressEntity)
        {
            AddressModel model = new AddressModel()
            {
                Id = addressEntity.Id,
                CreateAT = addressEntity.CreateAT,
                UpdateAt = addressEntity.UpdateAt,
                Street = addressEntity.Street,
                City = addressEntity.City,
                State = addressEntity.State,
                Country = addressEntity.Country,
                Number = addressEntity.Number,
            };

            return model;
        }

        private static AddressModel AddressDtoToAddressModel(AddressDto AddressDto)
        {
            AddressModel model = new AddressModel()
            {
                Street = AddressDto.Street,
                City = AddressDto.City,
                State = AddressDto.State,
                Country = AddressDto.Country,
                Number = AddressDto.Number,
            };

            return model;
        }

        private static AddressDto AddressModelToDto(AddressModel addressModel)
        {
            AddressDto dto = new AddressDto()
            {
                Street = addressModel.Street,
                City = addressModel.City,
                State = addressModel.State,
                Country = addressModel.Country,
                Number = addressModel.Number,
            };

            return dto;
        }

        public static void TransferDataEntity(CompanyEntity from, CompanyEntity to)
        {
            to.NumberCars = from.NumberCars;
            to.CNPJ = from.CNPJ;
            to.Name = from.Name;
            to.Address.Street = from.Address.Street;
            to.Address.City = from.Address.City;
            to.Address.Number = from.Address.Number;
            to.Address.Country = from.Address.Country;
            to.Address.State = from.Address.State;
            to.Address.UpdateAt = from.UpdateAt = DateTime.Now;
            to.NumberMotorcycies = from.NumberMotorcycies;
            to.Phone = from.Phone;
        }
    }
}