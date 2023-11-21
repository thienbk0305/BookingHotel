using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.RoomsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RoomsRepository : GenericRepository<BookingHotelDbContext, Room>, IRoomsRepository
    {
        public RoomsRepository(BookingHotelDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RoomsViewModel>> GetAllRoomsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from hotel in _context.Room
                 join images in _context.Image on hotel.ImgCodeByUserId equals images.Id into gj
                 from gi in gj.DefaultIfEmpty()
                 select new RoomsViewModel()
                 {
                     Id = hotel.Id,
                     RoomName = hotel.RoomName,
                     RoomSize = hotel.RoomSize,
                     RoomHuman = hotel.RoomHuman,
                     RoomType = hotel.RoomType,
                     Description = hotel.Description,
                     Active = hotel.Active,
                     Price = hotel.Price,
                     SysDate = hotel.SysDate,
                     ImgCodeByUserId = hotel.ImgCodeByUserId,
                     ImgCode = gi.ImgCode
                 }).ToListAsync();
            await _context.SaveChangesAsync(cancellation);
            return data!;
        }

        public async Task<RoomsViewModel> GetRoomDetailAsync(string id, CancellationToken cancellation)
        {
            var data = await
                (from hotel in _context.Room
                 join images in _context.Image on hotel.ImgCodeByUserId equals images.Id into gj
                 from gi in gj.DefaultIfEmpty()
                 where hotel.Id == id
                 select new RoomsViewModel()
                 {
                     Id = hotel.Id,
                     RoomName = hotel.RoomName,
                     RoomSize = hotel.RoomSize,
                     RoomHuman = hotel.RoomHuman,
                     RoomType = hotel.RoomType,
                     Description = hotel.Description,
                     Active = hotel.Active,
                     Price = hotel.Price,
                     SysDate = hotel.SysDate,
                     ImgCodeByUserId = hotel.ImgCodeByUserId,
                     ImgCode = gi.ImgCode
                 }).FirstOrDefaultAsync();
            await _context.SaveChangesAsync(cancellation);
            return data!;
        }
    }
}