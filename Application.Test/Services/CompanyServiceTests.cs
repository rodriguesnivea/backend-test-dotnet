using Microsoft.Extensions.Logging;
using Moq;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services;
using ParkingAPI.Services.Interfaces;
using ParkingAPI.Tracing.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Services
{
    public class CompanyServiceTests
    {
        private CompanyService _companyService;
        private Mock<ICompanyRepository> _companyRepositoryMock;
        private Mock<ILogger<CompanyService>> _loggerMock;
        private Mock<ITracingService> _tracingServiceMock;

        public CompanyServiceTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _loggerMock = new Mock<ILogger<CompanyService>>();
            _tracingServiceMock = new Mock<ITracingService>();
            _companyService = new CompanyService(_companyRepositoryMock.Object, _loggerMock.Object, _tracingServiceMock.Object);

        }

        [Fact]
        public async void CreateAsync_CriaEmpresaComSucesso()
        {
            var address = new AddressModel
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var companyTobeSaved = new CompanyModel
            { Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };

            var result = await _companyService.CreateAsync(companyTobeSaved);
            Assert.NotNull(result);
            Assert.Equal(companyTobeSaved.Id, result.Id);
            Assert.Equal(companyTobeSaved.Name, result.Name);
            Assert.Equal(companyTobeSaved.CNPJ, result.CNPJ);
            Assert.Equal(companyTobeSaved.NumberMotorcycies, result.NumberMotorcycies);
            Assert.Equal(companyTobeSaved.NumberCars, result.NumberCars);
            Assert.Equal(companyTobeSaved.Phone, result.Phone);
            Assert.Equal(companyTobeSaved.Address.Street, result.Address.Street);
            Assert.Equal(companyTobeSaved.Address.City, result.Address.City);
            Assert.Equal(companyTobeSaved.Address.State, result.Address.State);
            Assert.Equal(companyTobeSaved.Address.Country, result.Address.Country);
            Assert.Equal(companyTobeSaved.Address.Number, result.Address.Number);
        }

        [Fact]
        public async void DeleteAsync_DeletaEmpresaComSucesso()
        {
            Guid idToBeDeleed = Guid.NewGuid();
            _companyRepositoryMock.Setup(repository => repository.Exist(idToBeDeleed)).ReturnsAsync(true);
            await _companyService.DeleteAsync(idToBeDeleed);
            _companyRepositoryMock.Verify(repository => repository.DeleteCompanyAsync(idToBeDeleed), Times.Once());
        }

        [Fact]
        public async void DeleteAsync_LancaExcecaoVeiculoNaoEncontrado()
        {
            Guid idToBeDeleed = Guid.NewGuid();
            _companyRepositoryMock.Setup(repository => repository.Exist(idToBeDeleed)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _companyService.DeleteAsync(idToBeDeleed));
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
            _companyRepositoryMock.Verify(repository => repository.DeleteCompanyAsync(idToBeDeleed), Times.Never());
        }

        [Fact]
        public async void Exist_ValidaExistenciaDeUmaEmpresa()
        {
            Guid idToBeChecked = Guid.NewGuid();
            await _companyService.Exist(idToBeChecked);
            _companyRepositoryMock.Verify(repository => repository.Exist(idToBeChecked), Times.Once);
        }

        [Fact]
        public async void GetAllAsync_BuscaTodasEmpresasComSucesso()
        {
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };

            var expected = new List<CompanyEntity> { company };
            _companyRepositoryMock.Setup(repository => repository.FindAllAsync()).ReturnsAsync(expected);
            var result = await _companyService.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotNull(expected[0].Address);
            Assert.Equal(expected[0].Name, result[0].Name);
            Assert.Equal(expected[0].CNPJ, result[0].CNPJ);
            Assert.Equal(expected[0].NumberCars, result[0].NumberCars);
            Assert.Equal(expected[0].NumberMotorcycies, result[0].NumberMotorcycies);
            Assert.Equal(expected[0].Phone, result[0].Phone);

        }

        [Fact]
        public async void GetAllAsync_LancaExcecaoNaoContem()
        {
            _companyRepositoryMock.Setup(repositorry => repositorry.FindAllAsync()).ReturnsAsync(new List<CompanyEntity> { });
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _companyService.GetAllAsync());
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NO_CONTENT_EXCEPTION);
        }

        [Fact]
        public async void GetAsync_BuscaEmpresaComSucesso()
        {
            Guid id = Guid.NewGuid();
            var address = new AddressEntity
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var company = new CompanyEntity
            { Id = id, Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = address };

            _companyRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(true);
            _companyRepositoryMock.Setup(repository => repository.GetAsync(id)).ReturnsAsync(company);

            var result = await _companyService.GetAsync(id);
            Assert.NotNull(result);
            Assert.NotNull(result.Address);
            Assert.True(result.Id != default);
            Assert.Equal(company.Id, result.Id);
            Assert.Equal(company.Name, result.Name);
            Assert.Equal(company.CNPJ, result.CNPJ);
            Assert.Equal(company.NumberMotorcycies, result.NumberMotorcycies);
            Assert.Equal(company.NumberCars, result.NumberCars);
            Assert.Equal(company.Phone, result.Phone);

        }

        [Fact]
        public async void GetAsync_LancaExcecaoNaoEncontrado()
        {
            Guid id = Guid.NewGuid();
            _companyRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _companyService.GetAsync(id));
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
        }

        [Fact]
        public async void UpdateAsync_AtualizaEmpresaComSucesso()
        {
            var id = Guid.NewGuid();
            var addressTobeSaved = new AddressModel
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var companyTobeSaved = new CompanyModel
            { Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = addressTobeSaved };
            _companyRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(true);
            await _companyService.UpdateAsync(id, companyTobeSaved);
            _companyRepositoryMock.Verify(repository => repository.Exist(id), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_LancaExcecaoEmpresaNaoEncontrada()
        {
            var id = Guid.NewGuid();
            var addressTobeSaved = new AddressModel
            { Street = "Pedro S. Delton", City = "Rio Branco", State = "Ceara", Country = "Brasil", Number = 123 };
            var companyTobeSaved = new CompanyModel
            { Name = "CAutomotiva", CNPJ = "12539922385646", NumberMotorcycies = 2, NumberCars = 3, Phone = "(95) 3970-4294", Address = addressTobeSaved };
            _companyRepositoryMock.Setup(repository => repository.Exist(id)).ReturnsAsync(false);
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _companyService.UpdateAsync(id, companyTobeSaved));
            Assert.Equal(exception.ApplicationError, ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
        }
    }
}
