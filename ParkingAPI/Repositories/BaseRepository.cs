using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        public AppDbContext _Context;
        public DbSet<T> _DbSet;

        public BaseRepository(AppDbContext context)
        {
            _Context = context;
            _DbSet = context.Set<T>();  //seta dinamicamente que tipo será esse repository
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _DbSet.AddRangeAsync(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var result = _DbSet.SingleOrDefault(T => T.Id == id);
            _DbSet.Remove(result);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await  _DbSet.AnyAsync(T => T.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
           return  await _DbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _DbSet.FirstOrDefaultAsync(T => T.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _DbSet.FirstOrDefaultAsync(p => p.Id == entity.Id);

            _Context.Entry(result).CurrentValues.SetValues(entity);

            await _Context.SaveChangesAsync();
            return result;
        }
    }
   
}
