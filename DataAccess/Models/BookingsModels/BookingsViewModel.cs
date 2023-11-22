using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.BookingsModels
{
    public class BookingsViewModel
    {
        public string Id { get; set; }
        public string? CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? HRSId { get; set; }
        public string? HotelName { get; set; }
        public string? RoomName { get; set; }
        public StatusBooking Status { get; set; }
        public int Quantity { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

    }

}
