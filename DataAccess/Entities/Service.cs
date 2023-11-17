using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Service: IEntity
    {
        public Service()
        {
            Hotel = new HashSet<Hotel>();
            Room = new HashSet<Room>();
            HotelRoomService = new HashSet<HotelRoomService>();
        }

        public string Id { get; set; }
        public string? ServiceName { get; set; }
        public Type ServiceType { get; set; } = Type.Type_1;
        public string? ServiceContent { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<HotelRoomService> HotelRoomService { get; set; }

    }
    public enum Type : byte
    {
        [Display(Name = "Hotel")]
        Type_1 = 0,
        [Display(Name = "Room")]
        Type_2 = 1,
        [Display(Name = "Khác")]
        Type_3 = 2

    }
}
