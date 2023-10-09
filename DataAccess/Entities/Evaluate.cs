using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Evaluate
    {
        [Key]
        public int EId { get; set; }
        public string CusCode { get; set; } = null!;
        public string HotelCode { get; set; } = null!;
        public int? Rate { get; set; }
        public string? Description { get; set; }

        public virtual Customer CusCodeNavigation { get; set; } = null!;
        public virtual Hotel HotelCodeNavigation { get; set; } = null!;
    }
}
