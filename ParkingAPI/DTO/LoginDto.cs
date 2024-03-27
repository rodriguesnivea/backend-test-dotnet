using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class LoginDto
    {
        [Required()]
        [MaxLength(100)]
        [MinLength(254)]
        public string Email { get; set; }
        [Required()]
        public string password { get; set; }
    }
}
