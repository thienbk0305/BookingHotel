using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "FirstName is required")] 
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = null!;
        [EmailAddress][Required(ErrorMessage = "Email is required")] 
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")] 
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
