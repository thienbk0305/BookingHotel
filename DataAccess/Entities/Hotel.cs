using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Hotel : IEntity
    {
        public Hotel()
        {
            bookingDetail = new HashSet<BookingDetail>();
            Evaluate = new HashSet<Evaluate>();
            Room = new HashSet<Room>();
            SaleOff = new HashSet<SaleOff>();
            serviceDetail = new HashSet<ServiceDetail>();
        }
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        public string HotelCode { get; set; }
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;

        public virtual Hotel ImgCodeByUser { get; set; }
        [MaxLength(50)]
        public string? ImgCodeByUserId { get; set; }


        public virtual ICollection<Evaluate> Evaluate { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<SaleOff> SaleOff { get; set; }
        public virtual ICollection<ServiceDetail> serviceDetail { get; set; }
        public virtual ICollection<BookingDetail> bookingDetail { get; set; }
    }
}
