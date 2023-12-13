using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using ParkingAPI.Repositories;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using ParkingAPI.Tracing.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _VehicleRepository;
        private readonly ILogger<VehicleService> _logger;
        private readonly ITracingService _trace;

        public VehicleService(IVehicleRepository vehicleRepository, ILogger<VehicleService> logger, ITracingService trace)
        {
            _VehicleRepository = vehicleRepository;
            _logger = logger;
            _trace = trace;
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel model)
        {
            _logger.LogInformation($"m=CreateAsync, message=Iniciando registro de veiculo, trace={_trace.TraceId()}");
            model.Id = Guid.NewGuid();
            var entity = VehicleMap.ModelToEntity(model);
            await _VehicleRepository.CreateAsync(entity);
            _logger.LogInformation($"m=CreateAsync, vehicleid={entity?.Id}, message=Finlizano registro de veiculo, trace={_trace.TraceId()}");
            return model;

        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation($"m=DeleteAsync, vehicleid={id}, message=Iniciando remocao de veiculo, trace={_trace.TraceId()}");
            if (!await _VehicleRepository.Exist(id)) 
            {
                _logger.LogInformation($"m=DeleteAsync, vehicleid={id}, message=veiculo nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION);
            }            
            await _VehicleRepository.DeleteAsync(id);
            _logger.LogInformation($"m=DeleteAsync, vehicleid={id}, message=Finalizando remocao de veiculo, trace={_trace.TraceId()}");

        }

        public async Task<bool> Exist(Guid id)
        {
            _logger.LogInformation($"m=Exist, vehicleid={id}, message=Finalizando verificacao de existencia, trace={_trace.TraceId()}");
            return await _VehicleRepository.Exist(id);
        }

        public async Task<List<VehicleModel>> GetAllAsync()
        {
            _logger.LogInformation($"m=GetAllAsync, message=Iniciando busca por veiculos, trace={_trace.TraceId()}");
            var entities = await _VehicleRepository.GetAllAsync();
            if (entities.Count == 0)
            {
                _logger.LogError($"m=GetAllAsync, message=Nenhum veiculo disponivel, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_NO_CONTENT_EXCEPTION);
            }
            var models = entities.Select(entity => VehicleMap.EntityToModel(entity)).ToList();
            _logger.LogInformation($"m=GetAllAsync, message=Finalizando busca por veiculos, trace={_trace.TraceId()}");
            return models;
        }

        public async Task<VehicleModel> GetAsync(Guid id)
        {
            _logger.LogInformation($"m=GetAsync, message=Iniciando busca por veiculo pelo id={id}, trace={_trace.TraceId()}");
            if (!await Exist(id))
            {
                _logger.LogError($"m=GetAsync, vehicleid={id}, message=Veiculo nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION);
            }
            var entity = await _VehicleRepository.GetAsync(id);
            var model = VehicleMap.EntityToModel(entity);
            _logger.LogInformation($"m=GetAsync, message=Finalizando busca por veiculo pelo id={id}, trace={_trace.TraceId()}");
            return model;

        }

        public async Task<VehicleModel> UpdateAsync(Guid id, VehicleModel model)
        {
            _logger.LogInformation($"m=UpdateAsync, vehicleid={id}, message=Iniciando edicao de veiculo, trace={_trace.TraceId()}");
            if (!await Exist(id))
            {
                _logger.LogError($"m=UpdateAsync, vehicleid={id}, message=Veiculo nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION);
            }
            model.Id = id;
            var entity = VehicleMap.ModelToEntity(model);
            await _VehicleRepository.UpdateAsync(entity);
            _logger.LogInformation($"m=UpdateAsync, vehicleid={id}, message=Finalizando edicao de veiculo, trace={_trace.TraceId()}");
            return model;
        }
    }
}
