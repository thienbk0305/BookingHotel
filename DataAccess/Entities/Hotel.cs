using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Hotel
    {
        public Hotel()
        {
            Bookings = new HashSet<Booking>();
            Evaluates = new HashSet<Evaluate>();
            Policies = new HashSet<Policy>();
            Rooms = new HashSet<Room>();
            SaleOffs = new HashSet<SaleOff>();
            Services = new HashSet<Service>();
        }
        [Key]
        public int HotelId { get; set; }
        public string HotelCode { get; set; } = null!;
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }
        public string? ImgCode { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Image? ImgCodeNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<SaleOff> SaleOffs { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
