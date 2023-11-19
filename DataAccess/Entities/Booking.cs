using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Entities
{
    public class Booking : IEntity
    {
        public Booking()
        {
            BookingDetail = new HashSet<BookingDetail>();
        }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Status { get; set; }
        public long TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdateUser { get; set; }

        public virtual ICollection<BookingDetail> BookingDetail { get; set; }
    }
    public class OrderRequest
    {
        public string BookingId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderRequestData
    {
        public Customer customer { get; set; }
        public List<OrderRequest> orderItems { get; set; }
    }
}
