using System;
using System.Collections.Generic;

namespace ParkingAPI.DTO
{
    public class DetaildReportDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public int TotalCheckinCar { get; set; }
        public int TotalCheckoutCar { get; set; }
        public int TotalCheckinMotorcycle { get; set; }
        public int TotalCheckoutMotorcycle { get; set; }
        public List<ParkingHistoryDto> parkingHistory{ get; set; }
    }
}
