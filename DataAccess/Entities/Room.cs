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
        }
        public string Id { get; set; }  
        public string? RoomName { get; set; }
        public string? RoomSize { get; set; }
        public string? RoomHuman { get; set; }
        public string? RoomType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Hotel? HotelCodeByUser { get; set; }
        
        public string? HotelCodeByUserId { get; set; }

        public virtual Service? ServiceCodeByUser { get; set; }

        public string? ServiceCodeByUserId { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
    public enum Human : byte
    {
        [Display(Name = "2 người lớn")]
        Human_0 = 0,
        [Display(Name = "2 em bé")]
         Human_1 = 1,
        [Display(Name = "1 em bé")]
        Human_2 = 2,
        [Display(Name = "2 bé dưới 6 tuổi")]
        Human_3 = 3,
        [Display(Name = "1 bé dưới 12 tuổi")]
        Human_4 = 4
    }
    public enum Type : byte
    {
        [Display(Name = "1 giường đôi")]
        Type_0=0,
        [Display(Name = "2 giường đơn")]
        Type_1 = 1,
        [Display(Name = "1 giường đôi hoặc 2 giường đơn")]
        Type_2 = 2,
        [Display(Name = "2 giường đơn / 1 giường King")]
        Type_3 = 3,
        [Display(Name = "2 giường đơn / 1 giường Queen")]
        Type_4 = 4    ,
        [Display(Name = "1 giường đôi + 1 giường tầng")]
        Type_5 = 5
    }
}
