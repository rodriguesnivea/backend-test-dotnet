using ParkingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task <List<T>> GetAllAsync();
        Task <bool> Exist(Guid id);
    }
}
