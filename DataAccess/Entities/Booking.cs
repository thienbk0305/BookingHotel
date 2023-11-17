using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Booking : IEntity
    {
        
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        public string BookCode { get; set; }
        public bool Active { get; set; } = false;
        public bool? Deposit { get; set; }  
        public bool? Paid { get; set; }
        public string? Note { get; set; }

        public virtual Customer CusCodeByUser { get; set; }
        [MaxLength(50)]
        public string? CusCodeByUserId { get; set; }
        public virtual ICollection<BookingDetail> bookingDetail { get; set; }



    }
}
