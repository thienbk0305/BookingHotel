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
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public StatusBooking StatusBooking { get; set; } = StatusBooking.StatusBooking_1;
        public Double TotalAmount { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<BookingDetail> BookingDetail { get; set; }
    }

    public enum StatusBooking : byte
    {
        [Display(Name = "Đang Xử Lý")]
        StatusBooking_1 = 0,
        [Display(Name = "Thành Công")]
        StatusBooking_2 = 1,
        [Display(Name = "Từ Chối")]
        StatusBooking_3 = 2

    }
    public class OrderRequest
    {
        public string? BookingId { get; set; }
        public int Quantity { get; set; } = 1;
    }
    public class CreateOrderRequestData
    {
        public Customer? customer { get; set; }
        public List<OrderRequest>? orderItems { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
