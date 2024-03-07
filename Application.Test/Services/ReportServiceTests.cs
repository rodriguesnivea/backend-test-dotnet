using Microsoft.Extensions.Logging;
using Moq;
using ParkingAPI.Entities;
using ParkingAPI.Enums;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services;
using ParkingAPI.Tracing.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Services
{
    public class ReportServiceTests
    {
        private readonly ReportService _service;
        private Mock<IParkingRepository> _parkingMockRepository;
        private Mock<ICompanyRepository> _companyMockRepository;
        private Mock<ILogger<ReportService>> _loggerMock;
        private Mock<ITracingService> _tracingServiceMock;

        public ReportServiceTests()
        {
            _parkingMockRepository = new Mock<IParkingRepository>();
            _companyMockRepository = new Mock<ICompanyRepository>();
            _loggerMock = new Mock<ILogger<ReportService>>();
            _tracingServiceMock = new Mock<ITracingService>();
            _service = new ReportService(_companyMockRepository.Object, _parkingMockRepository.Object, _loggerMock.Object, _tracingServiceMock.Object);
        }

        [Fact] 
        public async Task GetdReportAsync_BuscaRelatorioComSucesso()
        {
            var companyId = Guid.NewGuid();
            var company = new CompanyEntity
            { Id = companyId, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = null };
            _companyMockRepository.Setup(repository => repository.Exist(companyId)).ReturnsAsync(true);
            _companyMockRepository.Setup(repository => repository.GetAsync(companyId)).ReturnsAsync(company);
            _parkingMockRepository.Setup(repository => repository.GetTotalVehicleCheckinsByCompany(companyId)).ReturnsAsync(1);
            _parkingMockRepository.Setup(repository => repository.GetTotalVehicleCheckoutsByCompany(companyId)).ReturnsAsync(1);
            var result = await _service.GetdReportAsync(companyId);
            Assert.NotNull(result);
            Assert.Equal(companyId, result.CompanyId);
            Assert.Equal(company.Name, result.CompanyName);
            Assert.Equal(company.CNPJ, result.Cnpj);
            Assert.Equal(1, result.TotalCheckinVehicles);
            Assert.Equal(1, result.TotalCheckinVehicles);
        }

        [Fact]
        public async Task GetdReportAsync_BuscaRelatorioLancaExcecaoEmpresaNaoEncontrada()
        {
            var companyId = Guid.NewGuid();
            _companyMockRepository.Setup(repository => repository.Exist(companyId)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _service.GetdReportAsync(companyId));
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
        }

        [Fact]
        public async Task GetDetaildReportAsync_BuscaDetalhesDoRelatorioComSucesso()
        {
            var companyId = Guid.NewGuid();   
            var company = new CompanyEntity
            { Id = companyId, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = null };
            var vehicle1 = new VehicleEntity { Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicle.Car };
            var vehicle2 = new VehicleEntity { Brand = "Bros", Model = "Grande", Color = "Azul", Plate = "ars-115", typeVehicle = TypeVehicle.Motocycle };
            var parking1 = new ParkingEntiy {CompanyId = Guid.NewGuid(), Vehicle = vehicle1, VehicleId = Guid.NewGuid(), CreateAT = DateTime.Now, UpdateAt = DateTime.Now };
            var parking2 = new ParkingEntiy {CompanyId = Guid.NewGuid(), Vehicle = vehicle2, VehicleId = Guid.NewGuid(), CreateAT = DateTime.Now, UpdateAt = DateTime.Now };
            var parkings = new List<ParkingEntiy>();
            parkings.Add(parking1);
            parkings.Add(parking2);
            _companyMockRepository.Setup(repository => repository.Exist(companyId)).ReturnsAsync(true);
            _companyMockRepository.Setup(repository => repository.GetAsync(companyId)).ReturnsAsync(company);
            _parkingMockRepository.Setup(repository => repository.GetTotalCarCheckinsByCompanyInTimeRange(companyId, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(1);
            _parkingMockRepository.Setup(repository => repository.GetTotalCarCheckoutsByCompanyInTimeRange(companyId, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(1);
            _parkingMockRepository.Setup(repository => repository.GetTotalMotorcycleCheckinsByCompanyInTimeRange(companyId, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(1);
            _parkingMockRepository.Setup(repository => repository.GetTotalMotorcycleCheckoutsByCompanyInTimeRange(companyId, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(1);
            _parkingMockRepository.Setup(repository => repository.GetParkingHistoryByCompanyInTimeRange(companyId, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(parkings);
            var result = await _service.GetDetaildReportAsync(companyId, DateTime.Now, DateTime.Now.AddDays(2));
            Assert.NotNull(result);
            Assert.Equal(company.Id, result.CompanyId);
            Assert.Equal(company.Name, result.CompanyName);
            Assert.Equal(company.CNPJ, result.Cnpj);
            Assert.Equal(1, result.TotalCheckinCar);
            Assert.Equal(1, result.TotalCheckoutCar);
            Assert.Equal(1, result.TotalCheckinMotorcycle);
            Assert.Equal(1, result.TotalCheckoutMotorcycle);
            Assert.Equal(parkings.Count, result.parkingHistory.Count);

        }

        [Fact]
        public async Task GetDetaildReportAsync_BuscaDetalhesLancaExcecaoEmpresaNaoEncontrada()
        {
            var companyId = Guid.NewGuid();
            _companyMockRepository.Setup(repository => repository.Exist(companyId)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _service.GetDetaildReportAsync(companyId, DateTime.Now, DateTime.Now.AddDays(2)));
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
        }

        [Fact]
        public async Task GetDetaildReportAsync_BuscaDetalhesLancaExcecaoBadRequest()
        {
            var companyId = Guid.NewGuid();
            var company = new CompanyEntity
            { Id = companyId, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = null };
            _companyMockRepository.Setup(repository => repository.Exist(companyId)).ReturnsAsync(true);
            _companyMockRepository.Setup(repository => repository.GetAsync(companyId)).ReturnsAsync(company);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _service.GetDetaildReportAsync(companyId, DateTime.Now.AddDays(2), DateTime.Now));
            Assert.Equal(exception.ApplicationError, ApplicationError.FILLED_REPORT_DATETIME_EXCEPTION);
        }
    }
}
