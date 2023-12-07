using ParkingAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Entities
{
    public class ParkingEntiy : BaseEntity
    {
        [Required]
        [Column("company_id")]
        public Guid CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

        [Required]
        [Column("vehicle_id")]
        public Guid VehicleId { get; set; }
        public virtual VehicleEntity Vehicle { get; set;}

        [Required]
        [Column("is_parked")]
        public bool IsParked { get; set; }

        public ParkingEntiy()
        {
        }

        public ParkingEntiy(Guid companyId, Guid vehicleId)
        {
            Id = Guid.NewGuid();
            CreateAT = DateTime.Now;
            UpdateAt = DateTime.Now;
            CompanyId = companyId;
            VehicleId = vehicleId;
            IsParked = true;

        }
    }
}
