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
    public class BookingsRepository : GenericRepository<BookingHotelDbContext, Booking>, IBookingsRepository
    {
        public BookingsRepository(BookingHotelDbContext context) : base(context)
        {
        }
    }
}
