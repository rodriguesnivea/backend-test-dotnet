using System;

namespace ParkingAPI.DTO
{
    public class ParkingHistoryDTO
    {
        public VehicleDTO vehicle { get; set;}
        public DateTime DateCheckin{ get; set;}
        public DateTime DateCheckout { get; set;}
        
    }
}
