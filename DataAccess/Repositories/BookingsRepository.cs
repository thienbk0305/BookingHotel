using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models.BookingsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BookingsRepository : GenericRepository<BookingHotelDbContext, Booking>, IBookingsRepository
    {
        private readonly BookingHotelDbContext _db;
        public BookingsRepository(BookingHotelDbContext context) : base(context)
        {
            _db = context;
        }
        /// Get all entities async<summary>
        /// </summary>
        /// <param name = "searchValue" ></ param >
        /// < returns ></ returns >
        public async Task<IEnumerable<BookingsViewModel>> GetAllBookingsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from bookings in _db.Booking
                 join bookingDetails in _db.BookingDetail on bookings.Id equals bookingDetails.BookingId
                 join hrs in _db.HotelRoomService on bookings.HRSId equals hrs.Id
                 join rooms in _db.Room on hrs.RoomId equals rooms.Id
                 join hotels in _db.Hotel on hrs.HotelId equals hotels.Id
                 join users in _db.User on bookings.CustomerId equals users.Id into usergroup
                 from ug in usergroup.DefaultIfEmpty()
                 join customers in _db.Customer on bookings.CustomerId equals customers.Id into customergroup
                 from cg in customergroup.DefaultIfEmpty()
                 select new BookingsViewModel()
                 {
                     Id = bookings.Id,
                     Status = bookings.StatusBooking,
                     TotalAmount = bookings.TotalAmount,
                     CreatedDate = bookings.CreatedDate,
                     Quantity = bookingDetails.Quantity,
                     HRSId =  bookings.HRSId,
                     HotelName = hotels.HotelName,
                     RoomName = rooms.RoomName,
                     CustomerId = ug.Id != null ? ug.Id : cg.Id,
                     FullName = ug.Id != null ? ug.FullName : cg.CusFullName,
                     CheckIn = bookings.CheckIn,
                     CheckOut = bookings.CheckOut
                 }).ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return data!;

        }

        ///// Get all entities async <summary>
        ///// </summary>
        ///// <param name="searchValue"></param>
        ///// <returns></returns>
        //public async Task<BookingsViewModel> GetBookingsDetailAsync(string searchValue, CancellationToken cancellation)
        //{
        //    if (searchValue == null)
        //    {
        //        searchValue = "";
        //    }

        //    var data = await
        //        (from hotels in _db.Hotel
        //         join hrs in _db.HotelRoomService on hotels.Id equals hrs.HotelId
        //         join rooms in _db.Room on hrs.RoomId equals rooms.Id
        //         join services in _db.Service on hrs.ServiceId equals services.Id
        //         join images in _db.Image on hotels.ImgCodeByUserId equals images.Id into imghotels
        //         from img in imghotels.DefaultIfEmpty()
        //         where hrs.Id == searchValue
        //         select new BookingsViewModel()
        //         {
        //             Id = hrs.Id,
        //             HotelId = hotels.Id,
        //             HotelName = hotels.HotelName,
        //             HotelAddress = hotels.HotelAddress,
        //             HotelLevel = hotels.HotelLevel,
        //             RoomId = rooms.Id,
        //             RoomName = rooms.RoomName,
        //             RoomSize = rooms.RoomSize,
        //             RoomHuman = rooms.RoomHuman,
        //             RoomType = rooms.RoomType,
        //             Status = hrs.Status,
        //             Price = hrs.Price,
        //             Active = hrs.Active,
        //             ServiceId = services.Id,
        //             ServiceName = services.ServiceName,
        //             ServiceType = services.ServiceType,
        //             ServiceContent = services.ServiceContent,
        //             HotelImage = img.ImgCode
        //         }).FirstOrDefaultAsync();
        //    return data!;

        //}
    }
}
