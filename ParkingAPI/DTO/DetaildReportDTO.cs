using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class DetaildReportDTO
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public int TotalCheckinCar { get; set; }
        public int TotalCheckoutCar { get; set; }
        public int TotalCheckinMotorcycle { get; set; }
        public int TotalCheckoutMotorcycle { get; set; }
        public List<ParkingHistoryDTO> parkingHistory{ get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime{ get; set; }
    }
}
