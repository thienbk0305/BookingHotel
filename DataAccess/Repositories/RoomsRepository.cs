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
    public class RoomsRepository : GenericRepository<BookingHotelDbContext, Room>, IRoomsRepository
    {
        public RoomsRepository(BookingHotelDbContext context) : base(context)
        {
        }
    }
}
