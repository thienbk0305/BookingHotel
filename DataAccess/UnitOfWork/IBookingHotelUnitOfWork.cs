using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IBookingHotelUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IAccountRepository AccountRepository { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
