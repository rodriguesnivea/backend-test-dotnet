using ParkingAPI.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ParkingAPI.DTO
{
    public class VehicleDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [StringLength(100)]
        public string Color { get; set; }

        [Required]
        [StringLength(100)]
        public string Plate { get; set; }

        [Required]
        public TypeVehicle typeVehicle { get; set; }
    }
}
