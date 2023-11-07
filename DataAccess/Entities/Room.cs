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
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        public string RoomCode { get; set; }
        public int? Floor { get; set; }
        public int? RoomMax { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual Hotel HotelCodeByUser { get; set; }
        [MaxLength(50)]
        public string? HotelCodeByUserId { get; set; }

        public virtual Hotel ImgCodeByUser { get; set; }
        [MaxLength(50)]
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
