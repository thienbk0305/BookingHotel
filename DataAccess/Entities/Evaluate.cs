using DataAccess.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Evaluate: IEntity
    {
        [Key]
        public string Id { get; set; }
        public int? Rate { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }= DateTime.Now;
        public virtual Customer? CusCodeByUser { get; set; }
        
        public string? CusCodeByUserId { get; set; }

        public virtual Hotel? HotelCodeByUser { get; set; }
        
        public string? HotelCodeByUserId { get; set; }
    }
}
