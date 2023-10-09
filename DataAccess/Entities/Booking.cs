using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Booking
    {
        [Key]
        public int BookId { get; set; }
        public string BookCode { get; set; } = null!;
        public string CusCode { get; set; } = null!;
        public string HotelCode { get; set; } = null!;
        public string RoomCode { get; set; } = null!;
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool? Active { get; set; }
        public bool? Deposit { get; set; }
        public bool? Paid { get; set; }
        public string? Note { get; set; }

        public virtual Customer CusCodeNavigation { get; set; } = null!;
        public virtual Hotel HotelCodeNavigation { get; set; } = null!;
        public virtual Room RoomCodeNavigation { get; set; } = null!;
    }
}
