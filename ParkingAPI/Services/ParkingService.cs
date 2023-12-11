using Microsoft.Extensions.Logging;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using ParkingAPI.Tracing.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly ILogger<ParkingService> _logger;
        private readonly ITracingService _trace;

        public ParkingService(IParkingRepository parkingRepository, ILogger<ParkingService> logger, ITracingService trace)
        {
            _parkingRepository = parkingRepository;
            _logger = logger;
            _trace = trace;
        }

        public async Task<bool> CheckinAsync(Guid companyId, Guid vehicleId)
        {
            _logger.LogInformation($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=Iniciando checki, trace={_trace.TraceId()}");
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);

            if (entity != null)
            {
                _logger.LogError($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=O Registro de estacionamento ja existe, trace={_trace.TraceId()}");
                return false;
            }

            entity = new ParkingEntiy(companyId, vehicleId);
            _logger.LogInformation($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=Finalizando checkin, trace={_trace.TraceId()}");
            return await _parkingRepository.CheckinAsync(entity); 
            
        }

        public async Task<bool> CheckoutAsync(Guid companyId, Guid vehicleId)
        {
            _logger.LogInformation($"m=CheckoutAsync, companyId={companyId}, vehicleId={vehicleId}, message=Iniciando checkout, trace={_trace.TraceId()}");
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);
            if (entity == null)
            {
                _logger.LogError($"m=CheckoutAsync, companyId={companyId}, vehicleId={vehicleId}, message=Registro de estacionamento nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.PARKING_NOT_FOUND_EXCEPTION);            
            }

            _logger.LogInformation($"m=CheckoutAsync, companyId={companyId}, vehicleId={vehicleId}, message=Finalizando checkout, trace={_trace.TraceId()}");
            return await _parkingRepository.CheckoutAsync(entity);
        }

        public async Task<ParkingModel> FindParkedVehicleAsync(Guid companyId, Guid vehicleId)
        {

            _logger.LogInformation($"m=FindParkedVehicleAsync, companyId={companyId}, vehicleId={vehicleId}, message=Iniciando busca por veiculo estacionado, trace={_trace.TraceId()}");
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);
            if (entity == null) 
            {
                _logger.LogError($"m=FindParkedVehicleAsync, companyId={companyId}, vehicleId={vehicleId}, message=Registro de estacionamento nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.PARKING_NOT_FOUND_EXCEPTION);
            }
            _logger.LogInformation($"m=FindParkedVehicleAsync, companyId={companyId}, vehicleId={vehicleId}, message=Finalizando busca por veiculo estacionado, trace={_trace.TraceId()}");
            return ParkingMap.EntityToModel(entity);
        }
    }
}
