using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class SaleOff : IEntity
    {
        [Key]
        public int Id { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? Datetime { get; set; }
        public DateTime? BeginDatetime { get; set; }
        public DateTime? ExpiryDatetime { get; set; }
        public int? Numbers { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }=false;

        public virtual Hotel HotelCodeByUser { get; set; }
        [MaxLength(50)]
        public string? HotelCodeByUserId { get; set; }
    }
}
