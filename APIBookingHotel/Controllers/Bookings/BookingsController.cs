using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.SystemsModels;
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

                    //chECK Thêm số lượng nếu có

                }

                //Kiểm tra xem đã có khách hàng nào trùng thông tin chưa ?

                var customerInfor = await _bookingHotelUnitOfWork.CustomerRepository.GetById(requestData.customer.CusPhone, HttpContext.RequestAborted);

                if (customerInfor == null)
                {
                    // TH1 : chưa có
                    // tạo khách hàng để lấy CustomerId
                    requestData.customer.Id = Common.Security.GenerateRandomId();
                    var cusID = await _bookingHotelUnitOfWork.CustomerRepository.Add(requestData.customer, HttpContext.RequestAborted);
                    if (cusID == null)
                    {
                        returnData.ResponseCode = "-1";
                        returnData.Description = "Dữ liệu không hợp lệ";
                        return Ok(returnData);
                    }

                    customerId = cusID.Id;

                }
                else
                {
                    // TH2 : đã có 
                    // Lấy customer theo thông tin khách nhập
                    customerId = customerInfor.Id;
                }


                foreach (var item in requestData.orderItems)
                {
                    var bookingDetail = await _bookingHotelUnitOfWork.SystemsRepository.GetSystemsDetailAsync(item.BookingId, HttpContext.RequestAborted);
                    totalAmount += bookingDetail.Price * item.Quantity;
                }
                // Tạo order 

                var order = new Booking
                {
                    Id = Common.Security.GenerateRandomId(),
                    TotalAmount = totalAmount,
                    CreatedDate = DateTime.Now,
                    CustomerId = customerId!,
                    CheckIn = requestData.CheckIn,
                    CheckOut = requestData.CheckOut,
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
                returnData.ResponseCode = "1"; ;
                returnData.Description = "Chúc mừng bạn đã tạo đơn hàng thành công !";
                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

