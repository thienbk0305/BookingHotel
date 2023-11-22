using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class LoginModel
    {
		[EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")] 
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
