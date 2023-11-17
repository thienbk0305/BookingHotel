using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ServiceDetail : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; } = false;



        public virtual Hotel HotelCodeByUser { get; set; }
        [MaxLength(50)]
        public string? HotelCodeByUserId { get; set; }


        public virtual Room RoomCodeByUser { get; set; }
        public virtual Service ServiceCodeByUser { get; set; }
    }
}
