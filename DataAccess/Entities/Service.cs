using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Service: IEntity
    {
        
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        public string ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }=false;

        public virtual Hotel HotelCodeByUser { get; set; }
        [MaxLength(50)]
        public string? HotelCodeByUserId { get; set; }

        public virtual Hotel ImgCodeByUser { get; set; }
        [MaxLength(50)]
        public string? ImgCodeByUserId { get; set; }
    }
}
