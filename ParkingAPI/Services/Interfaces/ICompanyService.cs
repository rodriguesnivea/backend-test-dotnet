using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ParkingAPI.Entities;
using ParkingAPI.Models;

namespace ParkingAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyModel> CreateAsync(CompanyModel model);
        Task<CompanyModel> UpdateAsync(Guid id, CompanyModel model);
        Task DeleteAsync(Guid id);
        Task<CompanyModel> GetAsync(Guid id);
        Task<List<CompanyModel>> GetAllAsync();
        Task<bool> Exist(Guid id);
    }
}
