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
      
        
        public virtual Hotel ImgCodeByUser { get; set; }
        [MaxLength(50)]
        public string? ImgCodeByUserId { get; set; }
        public virtual ICollection<ServiceDetail> serviceDetail { get; set; }
    }
}
