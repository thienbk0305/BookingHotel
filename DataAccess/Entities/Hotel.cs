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
            Evaluate = new HashSet<Evaluate>();
            Policy = new HashSet<Policy>();
            Room = new HashSet<Room>();
            SaleOff = new HashSet<SaleOff>();
            Service = new HashSet<Service>();
        }
        [Key]
        public int HotelId { get; set; }
        public string? HotelCode { get; set; }
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }
        public string? ImgCode { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Image? ImgCodeNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Evaluate> Evaluate { get; set; }
        public virtual ICollection<Policy> Policy { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<SaleOff> SaleOff { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}
