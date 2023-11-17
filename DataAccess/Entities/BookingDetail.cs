using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class BookingDetail : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        [MaxLength(50)]

        public string? HotelCodeByUserId { get; set; }
        public virtual Hotel HotelCodeByUser { get; set; }

        [MaxLength(50)]
        public string RoomCodeByUserId { get; set; }
        public virtual Room? RoomCodeByUser { get; set; }
        public virtual Booking BookingCodeByUser { get; set; }


    }
}
