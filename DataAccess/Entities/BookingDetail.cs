using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Entities
{
    public class BookingDetail: IEntity
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime CreateAt { get; set; }
    }

}
