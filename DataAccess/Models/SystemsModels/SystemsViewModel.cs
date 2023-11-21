using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Entities.HotelRoomService;

namespace DataAccess.Models.SystemsModels
{
    public class SystemsViewModel
    {
        public string? Id { get; set; }
        public double Price { get; set; }
        public DateTime? SysDate { get; set; }
        public Status Status { get; set; }
        public bool Active { get; set; } = false;

        public string? HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }

        public string? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomSize { get; set; }
        public string? RoomHuman { get; set; }
        public string? RoomType { get; set; }

        public string? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public Types ServiceType { get; set; }
        public string? ServiceContent { get; set; }

    }
    public class HRSViewModel
    {
        public string? Id { get; set; }
        public string? HotelId { get; set; }
        public string? RoomId { get; set; }
        public string? ServiceId { get; set; }
        public Status Status { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public DateTime? SysDate { get; set; }
    }
}
