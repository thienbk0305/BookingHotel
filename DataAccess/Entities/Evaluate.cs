using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Evaluate
    {
        [Key]
        public int EId { get; set; }
        public string? CusCode { get; set; }
        public string? HotelCode { get; set; }
        public int? Rate { get; set; }
        public string? Description { get; set; }

        public virtual Customer? CusCodeNavigation { get; set; }
        public virtual Hotel? HotelCodeNavigation { get; set; }
    }
}
