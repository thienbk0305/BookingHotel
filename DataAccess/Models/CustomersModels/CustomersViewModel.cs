using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.CustomersModels
{
    public class CustomersViewModel
    {
        public string? Id { get; set; }
        public string? CusFullName { get; set; }
        public string? Gender { get; set; }
        public string? CusEmail { get; set; }
        public DateTime? CusDob { get; set; }
        public string? CusPhone { get; set; }
        public string? CusAddress { get; set; }
        public string? CusDescription { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? SysDate { get; set; }

        public string? Id_Evaluate { get; set; }
        public string? Description { get; set; }
    }

}
