using DataAccess.Entities;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.RoomsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IRoomsRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<RoomsViewModel>> GetAllRoomsAsync(string searchValue, CancellationToken cancellation);
        Task<RoomsViewModel> GetRoomDetailAsync(string id, CancellationToken cancellation);
    }
}
