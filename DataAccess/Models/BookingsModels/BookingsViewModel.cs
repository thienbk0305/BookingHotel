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
        public string CustomerId { get; set; }
        public int Status { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }

    }

}
