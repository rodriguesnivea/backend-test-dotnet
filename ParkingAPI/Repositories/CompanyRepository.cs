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
            try
            {
                var result = _DbSet.Include(company => company.Address).Where(T => T.Id == id).SingleOrDefault();
                _DbSet.Remove(result);
                await _Context.SaveChangesAsync();

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
        public async Task<bool> CnpjExist(string cnpj)
        {
            return await _DbSet.AnyAsync(company => company.CNPJ.Equals(cnpj));
        }
        public async Task<CompanyEntity> GetByCnpjAsync(string cnpj)
        {
           return await _DbSet.Include(company => company.Address).FirstOrDefaultAsync( company => company.CNPJ.Equals(cnpj));
        }
    }
}
