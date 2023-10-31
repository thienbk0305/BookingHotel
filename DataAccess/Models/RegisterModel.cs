using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class RegisterModel
    {
        //[Required(ErrorMessage = "RoleName is required")]
        public string? RoleName { get; set; } = "User";
        //[Required(ErrorMessage = "FullName is required")]
        public string? FullName { get; set; } = "";
        [EmailAddress][Required(ErrorMessage = "Email is required")] 
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        //[Required(ErrorMessage = "ConfirmPassword is required")]
        public string? ConfirmPassword { get; set; }
    }
}
