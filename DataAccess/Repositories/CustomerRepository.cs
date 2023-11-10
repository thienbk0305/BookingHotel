using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class CustomerRepository : GenericRepository<BookingHotelDbContext, Customer>, ICustomerRepository
    {

        public CustomerRepository(BookingHotelDbContext context) : base(context)
        {
          
        }

        public async Task<int> InsertAsync(ContactView c, CancellationToken cancellation)
        {
            var cId = new SqlParameter("@c0", c.Id);
            var cFullName = new SqlParameter("@c1", c.CusFullName);
            cFullName.Value = String.IsNullOrEmpty(c.CusFullName) ? String.Empty : c.CusFullName;
            var cPhoneNumber = new SqlParameter("@c2", c.CusPhone);
            cPhoneNumber.Value = String.IsNullOrEmpty(c.CusPhone) ? String.Empty : c.CusPhone;
            var cEmail = new SqlParameter("@c3", c.CusEmail);
            cEmail.Value = String.IsNullOrEmpty(c.CusEmail) ? String.Empty : c.CusEmail;

            var eId = new SqlParameter("@e0", c.Id_Evaluate);
            var eDescription = new SqlParameter("@e1", c.Description);
            eDescription.Value = String.IsNullOrEmpty(c.Description) ? String.Empty : c.Description;

            return await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertContact @c0,@c1,@c2,@c3,@e0,@e1", cId, cFullName, cPhoneNumber, cEmail, eId, eDescription);
        }
    }
}
