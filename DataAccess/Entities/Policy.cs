using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Policy
    {
        [Key]
        public int PId { get; set; }
        public string PCode { get; set; } = null!;
        public string HotelCode { get; set; } = null!;
        public string? PName { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel HotelCodeNavigation { get; set; } = null!;
    }
}
