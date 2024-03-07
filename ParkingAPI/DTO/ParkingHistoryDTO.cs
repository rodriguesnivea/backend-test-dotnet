using System;

namespace ParkingAPI.DTO
{
    public class ParkingHistoryDto
    {
        public VehicleDto Vehicle { get; set;}
        public DateTime? DateCheckin{ get; set;}
        public DateTime? DateCheckout { get; set;}
        
    }
}
