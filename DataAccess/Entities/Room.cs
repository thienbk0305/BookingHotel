using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }
        [Key]
        public int RoomId { get; set; }
        public string? RoomCode { get; set; }
        public string? HotelCode { get; set; }
        public int? Floor { get; set; }
        public int? RoomMax { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImgCode { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel? HotelCodeNavigation { get; set; }
        public virtual Image? ImgCodeNavigation { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
