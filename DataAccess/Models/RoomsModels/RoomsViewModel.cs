using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.RoomsModels
{
    public class RoomsViewModel
    {
        public string Id { get; set; }
        public int? Floor { get; set; }
        public int? RoomMax { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime? SysDate { get; set; }

    }
}
