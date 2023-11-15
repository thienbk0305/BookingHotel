using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.HotelModels
{
    public class HotelsViewModel
    {
        public string Id { get; set; }
        public string? HotelName { get; set; }
        public string? HotelAddress { get; set; }
        public int? HotelLevel { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime? SysDate { get; set; }
    }
}
