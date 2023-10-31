using System.ComponentModel.DataAnnotations;

namespace APIBookingHotel.Models
{
    public class ProfileView
    {
        public string? Id { get; set; } 
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public string? Gender { get; set; }
        public string? NationID { get; set; }
        public string? Address { get; set; }
        public string? AvataImage { get; set; }
        public bool? Active { get; set; }
    }
}
