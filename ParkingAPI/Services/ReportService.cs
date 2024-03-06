using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkingAPI.DTO;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Mappers;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using ParkingAPI.Tracing.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly ILogger<ReportService> _logger;
        private readonly ITracingService _trace;

        public ReportService(ICompanyRepository companyRepository, IParkingRepository parkingRepository, ILogger<ReportService> logger, ITracingService trace)
        {
            _companyRepository = companyRepository;
            _parkingRepository = parkingRepository;
            _logger = logger;
            _trace = trace;
        }

        public async Task<ReportDTO> GetdReportAsync(Guid companyId)
        {
            _logger.LogInformation($"m=GetdReportAsync, message=Iniciando consulta ao relatorio de estacionamento, trace={_trace.TraceId()}");
            CompanyEntity company = await FindCompany(companyId);
            int totalCheckin = await _parkingRepository.GetTotalVehicleCheckinsByCompany(companyId);
            int totalCheckout = await _parkingRepository.GetTotalVehicleCheckoutsByCompany(companyId);
            var reportDTO = new ReportDTO
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                Cnpj = company.CNPJ,
                TotalCheckinVehicles = totalCheckin,
                TotalCheckoutVehicles = totalCheckout
            };

            _logger.LogInformation($"m=GetdReportAsync, message=Finalizando consulta ao relatorio de estacionamento, trace={_trace.TraceId()}");
            return reportDTO;
        }
        public async Task<DetaildReportDTO> GetDetaildReportAsync(Guid companyId, DateTime startTime, DateTime endTime)
        {
            _logger.LogInformation($"m=GetDetaildReportAsync, message=Iniciando consulta ao relatorio detalhado de estacionamento, trace={_trace.TraceId()}");
            CompanyEntity company = await FindCompany(companyId);
            (startTime, endTime) = HandleTimeRanger(startTime, endTime);
            int totalCheckinCar = await _parkingRepository.GetTotalCarCheckinsByCompanyInTimeRange(companyId, startTime, endTime);
            int totalCheckoutCar = await _parkingRepository.GetTotalCarCheckoutsByCompanyInTimeRange(companyId, startTime, endTime);
            int totalCheckinMotorcycle = await _parkingRepository.GetTotalMotorcycleCheckinsByCompanyInTimeRange(companyId, startTime, endTime);
            int totalCheckoutMotorcycle = await _parkingRepository.GetTotalMotorcycleCheckoutsByCompanyInTimeRange(companyId, startTime, endTime);
            var parkings = await _parkingRepository.GetParkingHistoryByCompanyInTimeRange(companyId, startTime, endTime);
            var parkingHistory = parkings
                .Select(p => new ParkingHistoryDTO
                {
                    Vehicle = VehicleMap.EntityToDto(p.Vehicle),
                    DateCheckin = p.CreateAT,
                    DateCheckout = p.UpdateAt
                }).ToList();

            var detaildReportDTO = new DetaildReportDTO
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                Cnpj = company.CNPJ,
                TotalCheckinCar = totalCheckinCar,
                TotalCheckoutCar = totalCheckoutCar,
                TotalCheckinMotorcycle = totalCheckinMotorcycle,
                TotalCheckoutMotorcycle = totalCheckoutMotorcycle,
                parkingHistory = parkingHistory
            };

            _logger.LogInformation($"m=GetDetaildReportAsync, message=Finalizando consulta ao relatorio detalhado de estacionamento, trace={_trace.TraceId()}");
            return detaildReportDTO;
        }

        private (DateTime, DateTime) HandleTimeRanger(DateTime startTime, DateTime endTime)
        {
            startTime = startTime.Date;
            endTime = endTime.Date;
            if (endTime < startTime)
            {
                _logger.LogError($"m=HandleTimeRanger, message=Data final '{endTime}' eh menor do que a data inicial '{startTime}', trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.FILLED_REPORT_DATETIME_EXCEPTION);

            }
            return (startTime, endTime.AddDays(1));
        }

        private async Task<CompanyEntity> FindCompany(Guid companyId)
        {
            if (!await _companyRepository.Exist(companyId))
            {
                _logger.LogError($"m=FindCompany, message=O objeto '{companyId}' nao foi encontrado na base de dados, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
            }
            return await _companyRepository.GetAsync(companyId);
        }
    }
}
