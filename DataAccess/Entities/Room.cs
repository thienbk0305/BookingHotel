using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Room : IEntity
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
            Hotel = new HashSet<Hotel>();
            Service = new HashSet<Service>();
            HotelRoomService = new HashSet<HotelRoomService>();
        }
        public string Id { get; set; }  
        public string? RoomName { get; set; }
        public string? RoomSize { get; set; }
        public string? RoomHuman { get; set; }
        public string? RoomType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Status_1;
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<HotelRoomService> HotelRoomService { get; set; }
    }
    public enum Status : byte
    {
        [Display(Name = "Phòng Mới")]
        Status_1 = 0,
        [Display(Name = "Phòng Hot")]
        Status_2 = 1,
        [Display(Name = "Phòng Giảm Giá")]
        Status_3 = 2

    }
}
