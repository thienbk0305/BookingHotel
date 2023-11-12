using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class BookingHotelUnitOfWork : IBookingHotelUnitOfWork
    {

        public IIdentityRepository Identity { get; }
        public IContactRepository ContactRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public INewsRepository NewsRepository { get; private set; }
        public IRoomsRepository RoomsRepository { get; private set; }

        private readonly BookingHotelDbContext _dbContext;

        public BookingHotelUnitOfWork (BookingHotelDbContext dbContext, IIdentityRepository identity)
        {
            _dbContext = dbContext;
            Identity = identity;
            ContactRepository = new ContactRepository(_dbContext);
            CustomerRepository = new CustomerRepository(_dbContext);
            NewsRepository = new NewsRepository(_dbContext);
            RoomsRepository = new RoomsRepository(_dbContext);
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
