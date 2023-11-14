using ParkingAPI.Entities;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyModel> CreateAsync(CompanyModel model)
        {
            if (await Exist(model.Id)) return null;

            var entity = new CompanyEntity(model);
            await _companyRepository.CreateAsync(entity);
            return CompanyMap.EntityToModel(entity);

        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _companyRepository.Exist(id))
            {
                await _companyRepository.DeleteCompanyAsync(id);
            }
        }

        public async Task<bool> Exist(Guid id)
        {
           return await _companyRepository.Exist(id);
        }

        public async Task<List<CompanyModel>> GetAllAsync()
        {
            var entities = await _companyRepository.FindAllAsync();
            var models = entities.Select(entity => CompanyMap.EntityToModel(entity)).ToList();
            return models;
        }

        public async Task<CompanyModel> GetAsync(Guid id)
        {   
            if (await Exist(id))
            {
                var entity = await _companyRepository.GetAsync(id);
                var  model = CompanyMap.EntityToModel(entity);

                return model;
            }

            return null;
        }

        public async Task<CompanyModel> UpdateAsync(Guid id, CompanyModel model)
        {
            if (await Exist(id))
            {
                model.Id = id;
                var entity = CompanyMap.ModelToEntity(model);
                await _companyRepository.UpdateCompanyAsync(entity);
                return CompanyMap.EntityToModel(entity);
            }

            return null;
        }
    }
}
