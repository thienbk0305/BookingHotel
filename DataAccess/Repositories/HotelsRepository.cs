using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.NewsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HotelsRepository : GenericRepository<BookingHotelDbContext, Hotel>, IHotelsRepository
    {
        public HotelsRepository(BookingHotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HotelsViewModel>> GetAllHotelsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from hotel in _context.Hotel
                 join images in _context.Image on hotel.ImgCodeByUserId equals images.Id into gj
                 from gi in gj.DefaultIfEmpty()
                 select new HotelsViewModel()
                 {
                     Id = hotel.Id,
                     HotelName = hotel.HotelName,
                     HotelAddress = hotel.HotelAddress,
                     HotelLevel = hotel.HotelLevel,
                     Description = hotel.Description,
                     Active = hotel.Active,
                     SysDate = hotel.SysDate,
                     ImgCodeByUserId = hotel.ImgCodeByUserId,
                     ImgCode = gi.ImgCode
                 }).ToListAsync();
            await _context.SaveChangesAsync(cancellation);
            return data!;
        }

        public async Task<HotelsViewModel> GetHotelDetailAsync(string id, CancellationToken cancellation)
        {
            var data = await
                (from hotel in _context.Hotel
                 join images in _context.Image on hotel.ImgCodeByUserId equals images.Id into gj
                 from gi in gj.DefaultIfEmpty()
                 where hotel.Id == id
                 select new HotelsViewModel()
                 {
                     Id = hotel.Id,
                     HotelName = hotel.HotelName,
                     HotelAddress = hotel.HotelAddress,
                     HotelLevel = hotel.HotelLevel,
                     Description = hotel.Description,
                     Active = hotel.Active,
                     SysDate = hotel.SysDate,
                     ImgCodeByUserId = hotel.ImgCodeByUserId,
                     ImgCode = gi.ImgCode
                 }).FirstOrDefaultAsync();
            await _context.SaveChangesAsync(cancellation);
            return data!;
        }
    }
}