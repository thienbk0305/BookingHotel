using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Entities
{
    public class BookingDetail: IEntity
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public string RoomId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
    }

}
