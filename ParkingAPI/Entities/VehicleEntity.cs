using Microsoft.EntityFrameworkCore;
using ParkingAPI.Enums;
using ParkingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Entities
{
    public class VehicleEntity : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("brand")]
        public string Brand { get; set; }

        [Required]
        [StringLength(100)]
        [Column("model")]
        public string Model { get; set; }

        [Required]
        [StringLength(100)]
        [Column("color")]
        public string Color { get; set; }

        [Required]
        [StringLength(100)]
        [Column("plate")]
        public string Plate { get; set; }

        [Required]
        [Column("type_vehicle")]
        public TypeVehicle typeVehicle { get; set; }

        public VehicleEntity()
        {
        }

        public VehicleEntity(VehicleModel model)
        {
            Id = model.Id;
            CreateAT = model.CreateAT;
            UpdateAt = model.UpdateAt;
            Brand = model.Brand;
            Model = model.Model;
            Color = model.Color;
            Plate = model.Plate;
            this.typeVehicle = model.typeVehicle;
        }
    }

}
