using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Menu : IEntity
    {
        //public Menu()
        //{
        //    New = new HashSet<New>();
        //}
        //[Key]
        public string Id { get; set; }
        public string? MenuName { get; set; }
        public int? MenuLevel { get; set; }
        public int? ParentId { get; set; }
        public bool? Visible { get; set; }
        public DateTime? SysDate { get; set; }

        //public virtual ICollection<New> New { get; set; }
    }
}
