using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class New: IEntity
    {
        public int Id { get; set; }
        [Key]
        [MaxLength(50)]
        public string NewsCode { get; set; }
        public string? Title { get; set; }
        public string? SumContent { get; set; }
        public string? NewsContent { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Source { get; set; }
        public int? CountView { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual Hotel ImgCodeByUser { get; set; }
        [MaxLength(50)]
        public string? ImgCodeByUserId { get; set; }

        public virtual Menu? Menu { get; set; }
        public int MenuId { get; set; }
    }
}
