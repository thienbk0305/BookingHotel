using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
       
        public int UserId { get; set; }
        public string UserCode { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string ImgCode { get; set; } = null!;
        public bool? ReadUser { get; set; }
        public bool? WriteUser { get; set; }
        public bool? DelUser { get; set; }
        public bool? Active { get; set; }
        public string? Description { get; set; }
        public DateTime SysDate { get; set; }

        public virtual Image ImgCodeNavigation { get; set; } = null!;
    }
}
