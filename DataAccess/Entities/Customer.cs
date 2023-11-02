﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Customer
    {
        public Customer()
        {
            Booking = new HashSet<Booking>();
            Evaluate = new HashSet<Evaluate>();
        }
        [Key]
        public int CusId { get; set; }
        public string? CusCode { get; set; }
        public string? CusFullName { get; set; }
        public string? Gender { get; set; }
        public string? CusEmail { get; set; }
        public DateTime? CusDob { get; set; }
        public string? CusPhone { get; set; }
        public string? CusAddress { get; set; }
        public string? CusDescription { get; set; }
        public bool? Active { get; set; }
        public DateTime? SysDate { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<Evaluate> Evaluate { get; set; }
    }
}
