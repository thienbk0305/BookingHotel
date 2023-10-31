using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class BookingHotelUnitOfWork : IBookingHotelUnitOfWork
    {

        public IIdentityRepository Identity { get; }

        private BookingHotelDbContext _dbContext;

        public BookingHotelUnitOfWork (BookingHotelDbContext dbContext, IIdentityRepository identity)
        {
            _dbContext = dbContext;
            Identity = identity;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
