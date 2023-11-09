using ParkingAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Models
{
    public class VehicleModel : BaseModel
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
        public TypeVehicleEnum typeVehicle { get; set; }

    }
}
