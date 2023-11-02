using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public Gender Gender { get; set; } = Gender.Gender1;
        public string? NationId { get; set; }
        public string? Address { get; set; }
        public string? AvatarImage { get; set; }
        public bool Active { get; set; }
        public bool PasswordChanged { get; set; }
        public DateTime? LastPasswordChanged { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
    public enum Gender : byte
    {
        [Display(Name = "None")]
        Gender1 = 0,
        [Display(Name = "Nam")]
        Gender2 = 0,
        [Display(Name = "Nữ")]
        Gender3 = 0
    }


}
