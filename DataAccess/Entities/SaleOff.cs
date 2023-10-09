using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class SaleOff
    {
        [Key]
        public int SaleId { get; set; }
        public string? HotelCode { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? Datetime { get; set; }
        public DateTime? BeginDatetime { get; set; }
        public DateTime? ExpiryDatetime { get; set; }
        public int? Numbers { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel? HotelCodeNavigation { get; set; }
    }
}
