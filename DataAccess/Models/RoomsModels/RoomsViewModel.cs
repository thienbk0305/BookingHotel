using DataAccess.Entities;
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
        public string? RoomName { get; set; }
        public string? RoomSize { get; set; }
        public string? RoomHuman { get; set; }
        public string? RoomType { get; set; }
        public Status Status { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

    }
}
