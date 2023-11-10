using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Image : IEntity
    {
        public Image()
        {
            Hotel = new HashSet<Hotel>();
            New = new HashSet<New>();
            Room = new HashSet<Room>();
            Service = new HashSet<Service>();
            User = new HashSet<User>();
        }
        public string Id { get; set; }

        public string ImgCode { get; set; }
        public string? PathServer { get; set; }
        public string? FileName { get; set; }
        public string? Description { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<New> New { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
