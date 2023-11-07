using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ContactRepository : GenericRepository<BookingHotelDbContext, Evaluate>,IContactRepository
    {

        public ContactRepository(BookingHotelDbContext context) : base(context)
        {
          
        }


    }
}
