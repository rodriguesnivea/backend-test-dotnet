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
using Xunit;

namespace Application.Tests.Service
{
    public class VehicleServiceTests
    {
        private VehicleService _vehicleService;
        private Mock<ILogger<VehicleService>> _loggerMock;
        private Mock<IVehicleRepository> _vehicleRepositoryMock;
        private Mock<ITracingService> _tracingServiceMock;

        public VehicleServiceTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _loggerMock = new Mock<ILogger<VehicleService>>();
            _tracingServiceMock = new Mock<ITracingService>();
            _vehicleService = new VehicleService(_vehicleRepositoryMock.Object, _loggerMock.Object, _tracingServiceMock.Object);
        }

        [Fact]
        public async void CreateAsync_CriaVeiculoComSucesso()
        {
            var vehicleToBeSaved = new VehicleModel { Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };
            _vehicleRepositoryMock.Setup(repository => repository.PlateExist(vehicleToBeSaved.Plate)).ReturnsAsync(false);
            var result = await _vehicleService.CreateAsync(vehicleToBeSaved);
            Assert.NotNull(result);
            Assert.Equal(vehicleToBeSaved.Id, result.Id);
            Assert.Equal(vehicleToBeSaved.Brand, result.Brand);
            Assert.Equal(vehicleToBeSaved.Model, result.Model);
            Assert.Equal(vehicleToBeSaved.Color, result.Color);
            Assert.Equal(vehicleToBeSaved.Plate, result.Plate);
            Assert.Equal(vehicleToBeSaved.typeVehicle, result.typeVehicle);
            Assert.True(result.Id != default);
        }

        [Fact]
        public async void CreateAsync_LancaExcecaoPlacaConflito()
        {
            var vehicleToBeSaved = new VehicleModel { Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };
            _vehicleRepositoryMock.Setup(repository => repository.PlateExist(vehicleToBeSaved.Plate)).ReturnsAsync(true);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.CreateAsync(vehicleToBeSaved));
            Assert.Equal(ApplicationError.VEHICLE_PLATE_CONFLICT_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void DeleteAsync_DeletaVeiculoComSucesso()
        {
            Guid idToBeDeleted = Guid.NewGuid();
            _vehicleRepositoryMock.Setup(repository => repository.Exist(idToBeDeleted)).ReturnsAsync(true);
            await _vehicleService.DeleteAsync(idToBeDeleted);
            _vehicleRepositoryMock.Verify(repository => repository.DeleteAsync(idToBeDeleted), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_LancaExcecaoVeiculoNaoEncontrado()
        {
            Guid idToBeDeleted = Guid.NewGuid();
            _vehicleRepositoryMock.Setup(repository => repository.Exist(idToBeDeleted)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.DeleteAsync(idToBeDeleted));
            Assert.Equal(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION, exception.ApplicationError);
            _vehicleRepositoryMock.Verify(repository => repository.DeleteAsync(idToBeDeleted), Times.Never);
        }

        [Fact]
        public async void Exist_ValidaExistenciaDeUmVeiculo()
        {
            Guid idToBeChecked = Guid.NewGuid();
            await _vehicleService.Exist(idToBeChecked);
            _vehicleRepositoryMock.Verify(repository => repository.Exist(idToBeChecked), Times.Once);
        }

        [Fact]
        public async void GetAllAsync_BuscaVeiculosComSucesso()
        {
            var expected = new List<VehicleEntity> { new VehicleEntity
            {Id = Guid.NewGuid(), Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car } };
            _vehicleRepositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(expected);
            var result = await _vehicleService.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.Equal(expected[0].Model, result[0].Model);
            Assert.Equal(expected[0].Brand, result[0].Brand);
            Assert.Equal(expected[0].Color, result[0].Color);
            Assert.Equal(expected[0].Plate, result[0].Plate);
            Assert.Equal(expected[0].typeVehicle, result[0].typeVehicle);
        }

        [Fact]
        public async void GetAllAsync_BuscaVeiculosLancaExcecaoNenhumVeiculoEncontrado()
        {
            var expected = new List<VehicleEntity>();
            _vehicleRepositoryMock.Setup(repository => repository.GetAllAsync()).ReturnsAsync(expected);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.GetAllAsync());
            Assert.Equal(ApplicationError.VEHICLE_NO_CONTENT_EXCEPTION, exception.ApplicationError);

        }
        [Fact]
        public async void GetAsync_BuscaVeiculoPorIdComSucesso()
        {
            var id = Guid.NewGuid();
            var expected = new VehicleEntity { Id = Guid.NewGuid(), Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };
            _vehicleRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(true);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(id)).ReturnsAsync(expected);

            var result = await _vehicleService.GetAsync(id);
            Assert.NotNull(result);
            Assert.Equal(expected.Brand, result.Brand);
            Assert.Equal(expected.Model, result.Model);
            Assert.Equal(expected.Color, result.Color);
            Assert.Equal(expected.Plate, result.Plate);
            Assert.Equal(expected.typeVehicle, result.typeVehicle);
            Assert.True(result.Id != default);
        }

        [Fact]
        public async void GetAsync_BuscaVeiculoPorIdLancaExcecao()
        {
            var id = Guid.NewGuid();
            _vehicleRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.GetAsync(id));
            Assert.Equal(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION, exception.ApplicationError);
            _vehicleRepositoryMock.Verify(repository => repository.GetAsync(id), Times.Never);
        }

        [Fact]
        public async void UpdateAsync_AtualizaTodosOsCamposExcetoPlacaDoVeiculoComSucesso()
        {
            var id = Guid.NewGuid();
            var vehicleToBeSaved = new VehicleModel { Brand = "Corola", Model = "Medio", Color = "Cinza", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Motocycle };
            var currentVehicle = new VehicleEntity { Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };
            _vehicleRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(true);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(id)).ReturnsAsync(currentVehicle);

            var result = await _vehicleService.UpdateAsync(id, vehicleToBeSaved);
            Assert.True(result.Id != default);
            Assert.NotNull(result);
            Assert.Equal(currentVehicle.Plate, result.Plate);
            Assert.NotEqual(currentVehicle.Brand, result.Brand);
            Assert.NotEqual(currentVehicle.Model, result.Model);
            Assert.NotEqual(currentVehicle.Color, result.Color);
            Assert.NotEqual(currentVehicle.typeVehicle, result.typeVehicle);
        }

        [Fact]
        public async void UpdateAsync_LancaExcecaoVeiculoNaoEncontrado()
        {
            var id = Guid.NewGuid();
            var vehicleToBeSaved = new VehicleModel { Brand = "Corola", Model = "Medio", Color = "Cinza", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Motocycle };
            _vehicleRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.UpdateAsync(id, vehicleToBeSaved));
            Assert.Equal(ApplicationError.VEHICLE_NOT_FOUND_EXCEPTION, exception.ApplicationError);
        }

        [Fact]
        public async void UpdateAsync_LancaExcecaoPlacaComConflitos()
        {
            var id = Guid.NewGuid();
            var vehicleToBeSaved = new VehicleModel { Id = id, Brand = "Corola", Model = "Medio", Color = "Cinza", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Motocycle };
            var currentVehicle = new VehicleEntity {Id = id, Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-345", typeVehicle = TypeVehicleEnum.Car };
            var vehicleFromPlate = new VehicleEntity {Id = Guid.NewGuid(), Brand = "Fiat", Model = "Grande", Color = "Azul", Plate = "ads-111", typeVehicle = TypeVehicleEnum.Car };
            _vehicleRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(true);
            _vehicleRepositoryMock.Setup(repository => repository.GetAsync(id)).ReturnsAsync(currentVehicle);
            _vehicleRepositoryMock.Setup(repository => repository.GetByPlateAsync(vehicleToBeSaved.Plate)).ReturnsAsync(vehicleFromPlate);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _vehicleService.UpdateAsync(id, vehicleToBeSaved));
            Assert.Equal(ApplicationError.VEHICLE_PLATE_CONFLICT_EXCEPTION, exception.ApplicationError);

        }
    }
}
