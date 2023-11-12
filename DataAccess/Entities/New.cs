using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class New : IEntity
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? SumContent { get; set; }
        public string? NewsContent { get; set; }
        public string? Source { get; set; }
        public int? CountView { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeByUser { get; set; }
        public string? ImgCodeByUserId { get; set; }

    }
}
