using ParkingAPI.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity>
    {
        Task DeleteCompanyAsync(Guid id);
        Task<List<CompanyEntity>> FindAllAsync();
        Task<CompanyEntity> UpdateCompanyAsync(CompanyEntity entity);
        Task<bool> CnpjExist(string cnpj);
        Task<CompanyEntity> GetByCnpjAsync(string cnpj);
    }
}
