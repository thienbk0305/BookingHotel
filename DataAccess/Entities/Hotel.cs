﻿using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Hotel : IEntity
    {
        public Hotel()
        {
            Room = new HashSet<Room>();
            Service = new HashSet<Service>();
            SaleOff = new HashSet<SaleOff>();
            HotelRoomService = new HashSet<HotelRoomService>();
        }
        public string Id { get; set; }
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Image ImgCodeByUser { get; set; }
        public string? ImgCodeByUserId { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<SaleOff> SaleOff { get; set; }
        public virtual ICollection<HotelRoomService> HotelRoomService { get; set; }
    }
}
