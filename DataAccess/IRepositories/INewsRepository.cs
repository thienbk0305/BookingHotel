using DataAccess.Entities;
using DataAccess.Models.NewsModels;
using DataAccess.Models.SystemsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface INewsRepository : IGenericRepository<New>
    {
        Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(string searchValue, CancellationToken cancellation);
        Task<NewsViewModel> GetNewsDetail(string id, CancellationToken cancellation);
    }
}
