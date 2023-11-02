using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class New
    {
        [Key]
        public int NewsId { get; set; }
        public string? NewsCode { get; set; }
        public string? Title { get; set; }
        public string? SumContent { get; set; }
        public string? NewsContent { get; set; }
        public string? ImgCode { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Source { get; set; }
        public int? CountView { get; set; }
        public int MenuId { get; set; }
        public string? LangCode { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeNavigation { get; set; }
        public virtual Language? LangCodeNavigation { get; set; }
        public virtual Menu? Menu { get; set; }
    }
}
