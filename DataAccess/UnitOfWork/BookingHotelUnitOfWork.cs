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
        public IServicesRepository ServicesRepository { get; private set; }
        public IHotelsRepository HotelsRepository { get; private set; }
        public IImagesRepository ImagesRepository { get; private set; }
        public IEvalutesRepository EvalutesRepository { get; private set; } 
        public IBookingsRepository BookingsRepository { get; private set; }
        public IBookingDetailsRepository BookingDetailsRepository { get; private set; }
        public ISystemsRepository SystemsRepository { get; private set; }

        private readonly BookingHotelDbContext _dbContext;

        public BookingHotelUnitOfWork (BookingHotelDbContext dbContext, IIdentityRepository identity)
        {
            _dbContext = dbContext;
            Identity = identity;
            ContactRepository = new ContactRepository(_dbContext);
            CustomerRepository = new CustomerRepository(_dbContext);
            NewsRepository = new NewsRepository(_dbContext);
            RoomsRepository = new RoomsRepository(_dbContext);
            ServicesRepository = new ServicesRepository(_dbContext);
            HotelsRepository = new HotelsRepository(_dbContext);
            ImagesRepository = new ImagesRepository(_dbContext);
            EvalutesRepository = new EvalutesRepository(_dbContext);
            BookingsRepository  = new BookingsRepository(_dbContext);
            BookingDetailsRepository = new BookingDetailsRepository(_dbContext);    
            SystemsRepository = new SystemsRepository(_dbContext);
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
