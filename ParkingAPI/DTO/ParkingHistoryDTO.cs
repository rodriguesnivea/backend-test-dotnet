using System;

namespace ParkingAPI.DTO
{
    public class ParkingHistoryDTO
    {
        public VehicleDTO Vehicle { get; set;}
        public DateTime? DateCheckin{ get; set;}
        public DateTime? DateCheckout { get; set;}
        
    }
}
