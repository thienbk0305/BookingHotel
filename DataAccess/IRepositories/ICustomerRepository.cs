using DataAccess.Entities;
using DataAccess.Models.CustomersModels;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<int> InsertAsync(CustomersViewModel c, CancellationToken cancellation);
    }
}
