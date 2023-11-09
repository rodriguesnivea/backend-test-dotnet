using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Models
{
    public class AddressModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Column("street")]
        public string Street { get; set; }
        [Required]
        [StringLength(100)]
        [Column("city")]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        [Column("state")]
        public string State { get; set; }
        [Required]
        [StringLength(100)]
        [Column("country")]
        public string Country { get; set; }

        [Required]
        [Column("number")]
        public int Number { get; set; }
         
    }
}
