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
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyService> _logger;
        private readonly ITracingService _trace;

        public CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger, ITracingService trace)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _trace = trace;
        }

        public async Task<CompanyModel> CreateAsync(CompanyModel model)
        {
            _logger.LogInformation($"m=CreateAsync, message=Iniciando registro de empresa, trace={_trace.TraceId()}");
            await ValidateCnpj(model);
            var entity = new CompanyEntity(model);
            await _companyRepository.CreateAsync(entity);
            _logger.LogInformation($"m=CreateAsync, message=Finalizando registro de empresa, trace={_trace.TraceId()}");
            return CompanyMap.EntityToModel(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation($"m=iniciando, message=Finalizando remocao de empresa, trace={_trace.TraceId()}");
            if (!await _companyRepository.Exist(id)) 
            {
                _logger.LogError($"m=DeleteAsync, message=O objeto '{id}' nao foi encontrado na base de dados, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
            }

            await _companyRepository.DeleteCompanyAsync(id);          
            _logger.LogInformation($"m=DeleteAsync, message=Finalizando remocao de empresa, trace={_trace.TraceId()}");
        }

        public async Task<bool> Exist(Guid id)
        {
            _logger.LogInformation($"m=Exist, message=Finalizando metodo exist, trace={_trace.TraceId()}");
            return await _companyRepository.Exist(id);
        }

        public async Task<List<CompanyModel>> GetAllAsync()
        {
            _logger.LogInformation($"m=GetAllAsync, message=Iniciando busca por todas empresas, trace={_trace.TraceId()}");
            var entities = await _companyRepository.FindAllAsync();
            if (entities.Count == 0)
            {
                _logger.LogError($"m=GetAllAsync, message=Nenhuma empresa disponivel, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NO_CONTENT_EXCEPTION);
            }
            var models = entities.Select(entity => CompanyMap.EntityToModel(entity)).ToList();
            _logger.LogInformation($"m=GetAllAsync, message=Finalizando busca por todas empresas, trace={_trace.TraceId()}");
            return models;
        }

        public async Task<CompanyModel> GetAsync(Guid id)
        {
            _logger.LogInformation($"m=GetAsync, message=Iniciando busca por empresa pelo id={id}, trace={_trace.TraceId()}");
            if (!await Exist(id))
            {
                _logger.LogError($"m=GetAsync, message=O objeto '{id}' nao foi encontrado na base de dados, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);           
            }
            var entity = await _companyRepository.GetAsync(id);
            var  model = CompanyMap.EntityToModel(entity);
            _logger.LogInformation($"m=GetAsync, message=Finalizando busca por empresa pelo id={id}, trace={_trace.TraceId()}");
            return model;
           
        }

        public async Task<CompanyModel> UpdateAsync(Guid id, CompanyModel model)
        {
            _logger.LogInformation($"m=UpdateAsync, message=Iniciando edicao da empresa '{id}', trace={_trace.TraceId()}");
            if (!await Exist(id))
            {
                _logger.LogError($"m=UpdateAsync, message=O objeto '{id}' nao foi encontrado na base de dados, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_NOT_FOUND_EXCEPTION);
            
            }

            model.Id = id;
            var entity = CompanyMap.ModelToEntity(model);
            await CheckLicenseCnpjConflict(id, model);
            await _companyRepository.UpdateCompanyAsync(entity);
            _logger.LogInformation($"m=UpdateAsync, message=Finalizando edicao da empresa '{id}', trace={_trace.TraceId()}");
            return CompanyMap.EntityToModel(entity);
        }

        private async Task ValidateCnpj(CompanyModel model)
        {
            if (await _companyRepository.CnpjExist(model.CNPJ))
            {
                _logger.LogError($"m=CreateAsync, message=Esse CNPJ pertence a outra empresa, trace={_trace.TraceId()}");
                throw new ServiceException(ApplicationError.COMPANY_CNPJ_CONFLICT_EXCEPTION);
            }
        }

        private async Task CheckLicenseCnpjConflict(Guid id, CompanyModel model)
        {
            var currentCompany = await _companyRepository.GetAsync(id);
            if(currentCompany.CNPJ != model.CNPJ)
            {
               var companyFromCnpj = await _companyRepository.GetByCnpjAsync(model.CNPJ);
               if(companyFromCnpj != null && currentCompany.Id != companyFromCnpj.Id)
                {
                    _logger.LogError($"m=UpdateAsync, message=Esse CNPJ pertence a outra empresa, trace={_trace.TraceId()}");
                    throw new ServiceException(ApplicationError.COMPANY_CNPJ_CONFLICT_EXCEPTION);
                }
            }
        }
    }
}
