using Microsoft.EntityFrameworkCore;
using ParkingAPI.Context;
using ParkingAPI.Entities;
using ParkingAPI.Repositories.Interfaces;

namespace ParkingAPI.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleEntity>, IVehicleRepository
    {
        //CONSTROI BaseRepository de Vehicle
        public VehicleRepository(AppDbContext context) : base(context) 
        {
        }
    }
}
