using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Service
    {
        [Key]
        public int SId { get; set; }
        public string SCode { get; set; } = null!;
        public string HotelCode { get; set; } = null!;
        public string? SName { get; set; }
        public string ImgCode { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel HotelCodeNavigation { get; set; } = null!;
        public virtual Image ImgCodeNavigation { get; set; } = null!;
    }
}
