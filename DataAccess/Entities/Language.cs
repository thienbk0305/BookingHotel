using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Language
    {
        public Language()
        {
            Menu = new HashSet<Menu>();
            New = new HashSet<New>();
        }
        [Key]
        public int LangId { get; set; }
        public string? LangCode { get; set; }
        public string? LangName { get; set; }
        public string? ImgCode { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual Image? ImgCodeNavigation { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<New> New { get; set; }
    }
}
