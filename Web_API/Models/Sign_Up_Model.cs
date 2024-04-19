using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    public class Sign_Up_Model
    {
        [Required]
        public String FirstName { get; set; } = null!;
        [Required]
        public String LastName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
