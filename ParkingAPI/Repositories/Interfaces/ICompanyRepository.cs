using ParkingAPI.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity> // HERDA IBaseRepository
    {
        Task DeleteCompanyAsync(Guid id);
        Task<List<CompanyEntity>> FindAllAsync();
        Task<CompanyEntity> UpdateCompanyAsync(CompanyEntity entity);
    }
}
