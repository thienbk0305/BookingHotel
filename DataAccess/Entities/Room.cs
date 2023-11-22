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
            Hotel = new HashSet<Hotel>();
            Service = new HashSet<Service>();
            HotelRoomService = new HashSet<HotelRoomService>();
        }
        public string Id { get; set; }  
        public string? RoomName { get; set; }
        public string? RoomSize { get; set; }
        public string? RoomHuman { get; set; }
        public string? RoomType { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<HotelRoomService> HotelRoomService { get; set; }
    }

}
