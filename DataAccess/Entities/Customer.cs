using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Evaluates = new HashSet<Evaluate>();
        }
        [Key]
        public int CusId { get; set; }
        public string CusCode { get; set; } = null!;
        public string? CusName { get; set; }
        public string? Gender { get; set; }
        public string? CusEmail { get; set; }
        public string? CusPassword { get; set; }
        public DateTime? CusDob { get; set; }
        public string? CusCard { get; set; }
        public string? CusPhone { get; set; }
        public string? CusAddress { get; set; }
        public string? CusAddress1 { get; set; }
        public string? CusDescription { get; set; }
        public bool? Active { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
    }
}
