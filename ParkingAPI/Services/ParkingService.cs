using Microsoft.Extensions.Logging;
using ParkingAPI.Entities;
using ParkingAPI.Enums;
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
        private readonly ICompanyRepository _companyRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public ParkingService(IParkingRepository parkingRepository, ILogger<ParkingService> logger, ITracingService trace, ICompanyRepository companyRepository, IVehicleRepository vehicleRepository)
        {
            _parkingRepository = parkingRepository;
            _logger = logger;
            _trace = trace;
            _companyRepository = companyRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task CheckinAsync(Guid companyId, Guid vehicleId)
        {
            _logger.LogInformation($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=Iniciando checki, trace={_trace.TraceId()}");
            var company = await _companyRepository.GetAsync(companyId);
            var vehicle = await _vehicleRepository.GetAsync(vehicleId);
            CheckValidParameters(company, vehicle);
            var entity = await _parkingRepository.FindParkedVehicleAsync(companyId, vehicleId);
            if (entity != null)
            {
                _logger.LogError($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=O Registro de estacionamento ja existe, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_ALREADY_PARKED_EXCEPTION);
            }
            await CheckAvailableSpots(company, vehicle);
            entity = new ParkingEntiy(companyId, vehicleId);
            _logger.LogInformation($"m=CheckinAsync, companyId={companyId}, vehicleId={vehicleId}, message=Finalizando checkin, trace={_trace.TraceId()}");
            await _parkingRepository.CheckinAsync(entity); 
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

        private async Task CheckAvailableSpots(CompanyEntity company, VehicleEntity vehicle)
        {           
            var parkedVehicleByType = await _parkingRepository.GetNumberOfParkedVehicles(company, vehicle);  
            
            if (vehicle.typeVehicle == TypeVehicleEnum.Car && parkedVehicleByType == company.NumberCars)
            {
                _logger.LogError($"m=CheckoutAsync, companyId={company.Id}, vehicleId={vehicle.Id}, message=Nenhuma vaga para carro disponivel, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.FILLED_CAR_SPOTS_EXCEPTION);
            }
            
            if(vehicle.typeVehicle == TypeVehicleEnum.Motocycle && parkedVehicleByType == company.NumberMotorcycies) 
            {
                _logger.LogError($"m=CheckoutAsync, companyId={company.Id}, vehicleId={vehicle.Id}, message=Nenhuma vaga para moto disponivel, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.FILLED_MOTORCYCLE_SPOTS_EXCEPTION);
            }
        }

        private void CheckValidParameters(CompanyEntity company, VehicleEntity vehicle)
        {
            if (company == null)
            {
                _logger.LogError($"m=CheckValidParameters, message=Registro de empresa nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
            }

            if(vehicle == null)
            {
                _logger.LogError($"m=CheckValidParameters, message=Registro de veiculo nao encontrado, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION);
            }
        }
    }
}
