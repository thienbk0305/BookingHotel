﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ContactView
    {
        public string? CusCode { get; set; }
        public string? CusFullName { get; set; }
        public string? CusEmail { get; set; }
        public string? CusPhone { get; set; }
        public string? Description { get; set; }
    }
}