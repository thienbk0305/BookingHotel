using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Entities
{
    public class Booking : IEntity
    {

        public string Id { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool Active { get; set; } = false;
        public bool? Deposit { get; set; }  
        public bool? Paid { get; set; }
        public string? Note { get; set; }

        public virtual Customer? CusCodeByUser { get; set; }
        public string? CusCodeByUserId { get; set; }

        public virtual Hotel? HotelCodeByUser { get; set; }
        public string? HotelCodeByUserId { get; set; }

        public virtual Room? RoomCodeByUser { get; set; }
        public string? RoomCodeByUserId { get; set; }


    }
}
