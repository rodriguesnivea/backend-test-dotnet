using System;

namespace ParkingAPI.Models
{
    public class ParkingModel : BaseModel
    {
        public Guid CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public Guid VehicleId { get; set; }
        public VehicleModel Vehicle { get; set; }
        public bool IsParked { get; set; }
    }
}
