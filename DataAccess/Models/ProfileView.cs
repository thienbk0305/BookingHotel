using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class ProfileView
    {
        public string? Id { get; set; } 
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? RoleId { get; set; }
        public int Gender { get; set; }
        public string? NationId { get; set; }
        public string? Address { get; set; }
        public string? AvatarImage { get; set; }
        public bool Active { get; set; }
    }
    public class ProfileViewById
    {
        public ProfileView? Data { get; set; }
        public List<string>? Messages { get; set; }
        public bool Succeeded { get; set; }
    }
}
