using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
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
    }
}
