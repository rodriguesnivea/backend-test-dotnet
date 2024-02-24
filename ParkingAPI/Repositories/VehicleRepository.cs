using Microsoft.EntityFrameworkCore;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleEntity>, IVehicleRepository
    {
        //CONSTROI BaseRepository de Vehicle
        public VehicleRepository(AppDbContext context) : base(context) 
        {
        }

        public Task<VehicleEntity> GetByPlateAsync(string plate)
        {
            return _DbSet.FirstOrDefaultAsync(x => x.Plate.Equals(plate));
        }

        public Task<bool> PlateExist(string plate)
        {
            return _DbSet.AnyAsync(v  => v.Plate.Equals(plate));
        }
    }
}
