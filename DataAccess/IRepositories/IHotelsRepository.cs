using DataAccess.Entities;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
        Task<IEnumerable<HotelsViewModel>> GetAllHotelsAsync(string searchValue, CancellationToken cancellation);
        Task<HotelsViewModel> GetHotelDetailAsync(string id, CancellationToken cancellation);
    }
}
