using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    public class Sign_In_Model
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}
