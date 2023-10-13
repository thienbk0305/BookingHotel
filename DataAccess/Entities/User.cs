using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ImgCode { get; set; } = null!;
        public bool? ReadUser { get; set; }
        public bool? WriteUser { get; set; }
        public bool? DelUser { get; set; }
        public bool? Active { get; set; }
        public string Description { get; set; } = null!;
    }

}
