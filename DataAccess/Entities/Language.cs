using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Language
    {
        public Language()
        {
            Menus = new HashSet<Menu>();
            News = new HashSet<New>();
        }
        [Key]
        public int LangId { get; set; }
        public string LangCode { get; set; } = null!;
        public string? LangName { get; set; }
        public string ImgCode { get; set; } = null!;
        public DateTime? SysDate { get; set; }

        public virtual Image ImgCodeNavigation { get; set; } = null!;
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<New> News { get; set; }
    }
}
