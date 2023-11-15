using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.ServicesModels
{
    public class ServicesViewModel
    {
        public string Id { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceContent { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime? SysDate { get; set; }
    }
}
