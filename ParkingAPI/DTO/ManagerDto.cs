using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class ManagerDto
    {
        [Required()]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required()]
        [MaxLength(100)]
        [MinLength(254)]
        public string Email { get; set; }
        [Required()]
        public string password { get; set; }
    }
}
