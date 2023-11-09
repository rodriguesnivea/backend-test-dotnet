using Microsoft.EntityFrameworkCore;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Mappers;
using ParkingAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyEntity>, ICompanyRepository
    {
        //CONSTROI BaseRepository de Company
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var result = _DbSet.Include(company => company.Address).FirstOrDefault(T => T.Id == id);
            _DbSet.Remove(result);
            await _Context.SaveChangesAsync();
        }

        public async Task<List<CompanyEntity>> FindAllAsync()
        {
            return await _DbSet.Include(company => company.Address).ToListAsync();
        }

        public async Task<CompanyEntity> UpdateCompanyAsync(CompanyEntity updatedEntity)
        {
            var entityFromDatabase = await _DbSet.Include(company => company.Address).FirstOrDefaultAsync(p => p.Id == updatedEntity.Id);
            CompanyMap.TransferDataEntity(updatedEntity, entityFromDatabase);
            await _Context.SaveChangesAsync();
            return entityFromDatabase;
        }
    }
}
