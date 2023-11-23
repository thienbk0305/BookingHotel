using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.BookingsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using static DataAccess.Models.Permissions;

namespace APIBookingHotel.Controllers.Bookings
{

    [Route("api/Bookings/")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public BookingsController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all Bookings from db
        //</summary>
        //
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetBookings")]
        [AllowAnonymous]

        public async Task<IActionResult> GetBookingsAsync(string? searchValue)
        {
            var bookings = await _bookingHotelUnitOfWork.BookingsRepository.GetAllBookingsAsync(searchValue!, HttpContext.RequestAborted);
            var result = _mapper.Map<List<BookingsViewModel>>(bookings);

            if (bookings != null)
            {
                return Ok(bookings);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("BookingsInsert")]
        [AllowAnonymous]
        public async Task<ActionResult> BoookingsInsert(CreateOrderRequestData requestData)
        {
            var errItems = "";
            double totalAmount = 0;
            var customerId = "";
            var returnData = new ReturnData();
            try
            {

                if (requestData == null)
                {
                    returnData.ResponseCode = "-1";
                    returnData.Description = "Dữ liệu không hợp lệ";
                    return Ok(returnData);
                }


                // Kiểm tra số lượng Booking?
                foreach (var item in requestData.orderItems!)
                {
                    var bookingDetail = await _bookingHotelUnitOfWork.SystemsRepository.GetById(item.BookingId!, HttpContext.RequestAborted);

                    if (bookingDetail == null)
                    {
                        errItems += item.BookingId + "";
                        continue;
                    }

                }

                //Kiểm tra xem đã có khách hàng nào trùng thông tin chưa ?
                var userInfor = await _bookingHotelUnitOfWork.Identity.GetById(requestData.customer.CusEmail, HttpContext.RequestAborted);
                if (userInfor == null)
                {
                    var customerInfor = await _bookingHotelUnitOfWork.CustomerRepository.GetById(requestData.customer.CusEmail, HttpContext.RequestAborted);

                    if (customerInfor == null)
                    {
                        // tạo khách hàng để lấy CustomerId
                        requestData.customer.Id = Common.Security.GenerateRandomId();
                        requestData.customer.SysDate = DateTime.Now;
                        var cusId = await _bookingHotelUnitOfWork.CustomerRepository.Add(requestData.customer, HttpContext.RequestAborted);
                        if (cusId == null)
                        {
                            returnData.ResponseCode = "-1";
                            returnData.Description = "Dữ liệu không hợp lệ";
                            return Ok(returnData);
                        }
                        customerId = cusId.Id;
                    }
                    else
                    {
                        customerId = customerInfor.Id;
                    }
                }
                else
                {
                    customerId = userInfor.Id;
                }

                foreach (var item in requestData.orderItems)
                {
                    var bookingDetail = await _bookingHotelUnitOfWork.SystemsRepository.GetSystemsDetailAsync(item.BookingId!, HttpContext.RequestAborted);
                    totalAmount += bookingDetail.Price * item.Quantity;
                }
                // Tạo order 

                var order = new Booking
                {
                    Id = Common.Security.GenerateRandomId(),
                    TotalAmount = totalAmount,
                    CreatedDate = DateTime.Now,
                    CustomerId = customerId!,
                    HRSId = requestData.orderItems[0].BookingId!,
                    CheckIn = requestData.orderItems[0].CheckIn,
                    CheckOut = requestData.orderItems[0].CheckOut,
                };

                var orderId = await _bookingHotelUnitOfWork.BookingsRepository.Add(order, HttpContext.RequestAborted);
                if (orderId != null)
                {
                    // tạo order Detail
                    foreach (var item in requestData.orderItems)
                    {
                        var orderDetail = new BookingDetail
                        {
                            Id = Common.Security.GenerateRandomId(),
                            BookingId = orderId.Id,
                            Quantity = item.Quantity,
                            CreateAt = DateTime.Now,
                        };
                        await _bookingHotelUnitOfWork.BookingDetailsRepository.Add(orderDetail, HttpContext.RequestAborted);
                    }
                }
                //returnData.ResponseCode = "1"; ;
                //returnData.Description = "Chúc mừng bạn đã tạo đơn hàng thành công !";
                return Ok(1);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

