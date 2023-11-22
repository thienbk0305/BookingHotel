using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.SystemsModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.IRepositories
{
    public interface ISystemsRepository : IGenericRepository<HotelRoomService>
    {
        Task<IEnumerable<SystemsViewModel>> GetAllSystemsAsync(string searchValue, CancellationToken cancellation);
        Task<int> UpdateSystemsAsync(HRSViewModel hrs, CancellationToken cancellation);
        Task<SystemsViewModel> GetSystemsDetailAsync(string searchValue, CancellationToken cancellation);
    }
}