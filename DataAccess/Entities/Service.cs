using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Service
    {
        [Key]
        public int SId { get; set; }
        public string? SCode { get; set; }
        public string? HotelCode { get; set; }
        public string? SName { get; set; }
        public string? ImgCode { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel? HotelCodeNavigation { get; set; }
        public virtual Image? ImgCodeNavigation { get; set; }
    }
}
