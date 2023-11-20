using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.SystemsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SystemsRepository : GenericRepository<BookingHotelDbContext, HotelRoomService>, ISystemsRepository
    {
        private readonly BookingHotelDbContext _db;
        public SystemsRepository(BookingHotelDbContext context) : base(context)
        {
            _db = context;
        }
        /// Get all entities async <summary>
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemsViewModel>> GetAllSystemsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from hotels in _db.Hotel
                 join hrs in _db.HotelRoomService on hotels.Id equals hrs.HotelId
                 join rooms in _db.Room on hrs.RoomId equals rooms.Id
                 join services in _db.Service on hrs.ServiceId equals services.Id
                 select new SystemsViewModel()
                 {
                     Id = hrs.Id,
                     HotelId = hotels.Id,
                     HotelName = hotels.HotelName,
                     HotelAddress = hotels.HotelAddress,
                     HotelLevel = hotels.HotelLevel,
                     RoomId = rooms.Id,
                     RoomName = rooms.RoomName,
                     RoomSize = rooms.RoomSize,
                     RoomHuman = rooms.RoomHuman,
                     RoomType = rooms.RoomType,
                     Status = rooms.Status,
                     Price = rooms.Price,
                     Active = rooms.Active,
                     ServiceId = services.Id,
                     ServiceName = services.ServiceName,
                     ServiceType = services.ServiceType,
                     ServiceContent = services.ServiceContent
                 }).ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return data!;

        }
    }
}
