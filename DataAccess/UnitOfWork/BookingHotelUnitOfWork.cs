using DataAccess.DBContext;
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
        public IAccountRepository AccountRepository { get; }
        private BookingHotelDbContext _dbContext;

        public BookingHotelUnitOfWork (BookingHotelDbContext dbContext,IAccountRepository accountRepository)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
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
