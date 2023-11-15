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
    public class ImagesRepository : GenericRepository<BookingHotelDbContext, Image>, IImagesRepository
    {
        public ImagesRepository(BookingHotelDbContext context) : base(context)
        {
        }
    }
}
