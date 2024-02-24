using Microsoft.Extensions.Logging;
using Moq;
using ParkingAPI.Entities;
using ParkingAPI.Enums;
using ParkingAPI.Exceptions;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services;
using ParkingAPI.Tracing.Interfaces;
using System;
using Xunit;

namespace Application.Test.Services
{
    public class ParkingAPIServiceTests
    {
        private readonly ParkingService _parkingService;
        private Mock<IParkingRepository> _parkingRepositoryMock;
        private Mock<IVehicleRepository> _vehicleRepositoryMock;
        private Mock<ICompanyRepository> _CompanyRepositoryMock;
        private Mock<ILogger<ParkingService>> _loggerMock;
        private Mock<ITracingService> _tracingMock;

        public ParkingAPIServiceTests() 
        {
            _parkingRepositoryMock = new Mock<IParkingRepository>();
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _CompanyRepositoryMock = new Mock<ICompanyRepository>();
            _loggerMock = new Mock<ILogger<ParkingService>>();
            _tracingMock = new Mock<ITracingService>();
            _parkingService = new ParkingService(_parkingRepositoryMock.Object, _loggerMock.Object, _tracingMock.Object, _CompanyRepositoryMock.Object, _vehicleRepositoryMock.Object);
        }

        [Fact]
        public async void CheckinAsync_RealizaCheckinComSucesso()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            ParkingEntiy parkingEntityNull = null;
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = idCompany, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };
            var vehicle = new VehicleEntity { Id = idVehicle, Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };

            var parkedVehicleByType = company.NumberCars - 2; 
            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(idVehicle)).ReturnsAsync(vehicle);
            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(parkingEntityNull);
            _parkingRepositoryMock.Setup(repository => repository.GetNumberOfParkedVehicles( company, vehicle)).ReturnsAsync(parkedVehicleByType);
            await _parkingService.CheckinAsync(idCompany, idVehicle);

            _parkingRepositoryMock.Verify(repository => repository.CheckinAsync(It.IsAny<ParkingEntiy>()), Times.Once());

        }

        [Fact]
        public async void CheckinAsync_LancaExcecaoEmpresaNaoEncontrada()
        { 
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            CompanyEntity company = null;

            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckinAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void CheckinAsync_LancaExcecaoVeiculoNaoEncontrado()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            VehicleEntity vehicle = null;
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = idCompany, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };

            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(idVehicle)).ReturnsAsync(vehicle);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckinAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void CheckinAsync_LancaExcecaoRegistroDeEstacionamentoExistente()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = idCompany, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };
            var vehicle = new VehicleEntity { Id = idVehicle, Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };

            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(idVehicle)).ReturnsAsync(vehicle);
            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(new ParkingEntiy(idCompany,idVehicle));

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckinAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.VEHICLE_ALREADY_PARKED_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void CheckinAsync_LancaExcecaoVagaEsgotadaParaCarro()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            ParkingEntiy parkingEntityNull = null;
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = idCompany, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };
            var vehicle = new VehicleEntity { Id = idVehicle, Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };

            var parkedVehicleByType = company.NumberCars;
            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(idVehicle)).ReturnsAsync(vehicle);
            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(parkingEntityNull);
            _parkingRepositoryMock.Setup(repository => repository.GetNumberOfParkedVehicles(company, vehicle)).ReturnsAsync(parkedVehicleByType);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckinAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.FILLED_CAR_SPOTS_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void CheckinAsync_LancaExcecaoVagaEsgotadaParaMoto()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            ParkingEntiy parkingEntityNull = null;
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = idCompany, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };
            var vehicle = new VehicleEntity { Id = idVehicle, Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Motocycle };

            var parkedVehicleByType = company.NumberMotorcycies;
            _CompanyRepositoryMock.Setup(repository => repository.GetAsync(idCompany)).ReturnsAsync(company);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(idVehicle)).ReturnsAsync(vehicle);
            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(parkingEntityNull);
            _parkingRepositoryMock.Setup(repository => repository.GetNumberOfParkedVehicles(company, vehicle)).ReturnsAsync(parkedVehicleByType);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckinAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.FILLED_MOTORCYCLE_SPOTS_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void CheckoutAsync_RealizaCheckoutComSucesso()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            ParkingEntiy parking = new ParkingEntiy(idCompany, idVehicle);

            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(parking);

            await _parkingService.CheckoutAsync(idCompany, idVehicle);
            _parkingRepositoryMock.Verify(repository => repository.CheckoutAsync(parking), Times.Once());
        }

        [Fact]
        public async void CheckoutAsync_LancaExcecaoEstacionamentoNaoEncontrado()
        {
            var idCompany = Guid.NewGuid();
            var idVehicle = Guid.NewGuid();
            ParkingEntiy parking = null;

            _parkingRepositoryMock.Setup(repository => repository.FindParkedVehicleAsync(idCompany, idVehicle)).ReturnsAsync(parking);

            var exception = await Assert.ThrowsAsync<ServiceException>(() => _parkingService.CheckoutAsync(idCompany, idVehicle));
            Assert.Equal(ApplicationError.PARKING_NOT_FOUND_EXCEPTION, exception.ApplicationError);

            _parkingRepositoryMock.Verify(repository => repository.CheckoutAsync(parking), Times.Never());
        }

    }
}
