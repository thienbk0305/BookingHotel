using DataAccess.Entities;
using DataAccess.Models.BookingsModels;
using DataAccess.Models.SystemsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBookingsRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<BookingsViewModel>> GetAllBookingsAsync(string searchValue, CancellationToken cancellation);
        Task<BookingsViewModel> GetBookingsDetailAsync(string searchValue, CancellationToken cancellation);
    }
}
