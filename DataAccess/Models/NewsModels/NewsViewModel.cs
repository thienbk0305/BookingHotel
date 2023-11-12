using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.NewsModels
{
    public class NewsViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? SumContent { get; set; }
        public string? NewsContent { get; set; }
        public string? Source { get; set; }
        public bool Active { get; set; }
        public DateTime? SysDate { get; set; }

    }
}
