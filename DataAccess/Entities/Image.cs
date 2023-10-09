using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Image
    {
        public Image()
        {
            Hotels = new HashSet<Hotel>();
            Languages = new HashSet<Language>();
            News = new HashSet<New>();
            Rooms = new HashSet<Room>();
            Services = new HashSet<Service>();
            Users = new HashSet<User>();
        }
        [Key]
        public int ImgId { get; set; }
        public string ImgCode { get; set; } = null!;
        public string? PathServer { get; set; }
        public string? FileName { get; set; }
        public string? Description { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<New> News { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
