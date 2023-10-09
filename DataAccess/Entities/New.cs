using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class New
    {
        [Key]
        public int NewsId { get; set; }
        public string NewsCode { get; set; } = null!;
        public string? Title { get; set; }
        public string? SumContent { get; set; }
        public string? NewsContent { get; set; }
        public string ImgCode { get; set; } = null!;
        public DateTime? Datetime { get; set; }
        public string? Source { get; set; }
        public int? CountView { get; set; }
        public int MenuId { get; set; }
        public string LangCode { get; set; } = null!;
        public DateTime? SysDate { get; set; }

        public virtual Image ImgCodeNavigation { get; set; } = null!;
        public virtual Language LangCodeNavigation { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
    }
}
