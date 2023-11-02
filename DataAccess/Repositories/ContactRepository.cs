using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ContactRepository : IContactRepository<Evaluate>
    {
        private readonly BookingHotelDbContext _context;

        public ContactRepository(BookingHotelDbContext context)
        {
            _context = context;
        }


    }
}
