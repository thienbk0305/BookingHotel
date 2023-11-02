using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Image
    {
        public Image()
        {
            Hotel = new HashSet<Hotel>();
            Language = new HashSet<Language>();
            New = new HashSet<New>();
            Room = new HashSet<Room>();
            Service = new HashSet<Service>();
            User = new HashSet<User>();
        }
        [Key]
        public int ImgId { get; set; }
        public string? ImgCode { get; set; }
        public string? PathServer { get; set; }
        public string? FileName { get; set; }
        public string? Description { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual ICollection<Hotel> Hotel { get; set; }
        public virtual ICollection<Language> Language { get; set; }
        public virtual ICollection<New> New { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
