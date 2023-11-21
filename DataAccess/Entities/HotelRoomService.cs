using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class HotelRoomService: IEntity
    {
        public string Id { get; set; }
        public string? HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public string? RoomId { get; set; }
        public Room? Room { get; set; }

        public string? ServiceId { get; set; }
        public Service? Service { get; set; }

        public double Price { get; set; }
        public Status Status { get; set; } = Status.Status_1;
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

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
