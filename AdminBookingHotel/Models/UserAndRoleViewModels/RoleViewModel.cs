using System.ComponentModel.DataAnnotations;

namespace AdminBookingHotel.Models.UserAndRoleViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? RoleName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? RoleNomalizedName { get; set; }
        public bool Selected { get; set; }

    }
}
