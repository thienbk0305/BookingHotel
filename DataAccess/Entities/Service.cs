using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Service: IEntity
    {
        
        public string Id { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceContent { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Hotel? HotelCodeByUser { get; set; }
        
        public string? HotelCodeByUserId { get; set; }

        public virtual Room? RoomCodeByUser { get; set; }

        public string? RoomCodeByUserId { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        
        public string? ImgCodeByUserId { get; set; }
    }
}
