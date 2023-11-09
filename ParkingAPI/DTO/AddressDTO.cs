using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class AddressDTO
    {
        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string State { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
