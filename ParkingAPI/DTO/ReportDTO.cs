using System;
using System.Collections.Generic;

namespace ParkingAPI.DTO
{
    public class ReportDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public int TotalCheckinVehicles { get; set; }
        public int TotalCheckoutVehicles { get; set; }
    }
}
