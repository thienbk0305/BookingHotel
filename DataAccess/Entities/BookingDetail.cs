using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Entities
{
    public class BookingDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public string BookingId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
    }

}
