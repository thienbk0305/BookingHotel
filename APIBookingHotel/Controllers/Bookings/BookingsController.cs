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

        //[HttpPost]
        //[Route("BookingsInsert")]
        //[AllowAnonymous]
        //public async Task<ActionResult> BoookingsInsert(CreateOrderRequestData requestData)
        //{
        //    var errItems = "";
        //    decimal totalAmount = 0;
        //    var customerId = "";
        //    var returnData = new ReturnData();
        //    try
        //    {

        //        if (requestData == null)
        //        {
        //            returnData.ResponseCode = "-1";
        //            returnData.Description = "Dữ liệu không hợp lệ";
        //            return Ok(returnData);
        //        }


        //        // Kiểm tra xem giá tiền của Booking có khớp với DB không?
        //        foreach (var item in requestData.orderItems)
        //        {
        //            var bookingDetail = await _bookingHotelUnitOfWork.SystemsRepository.GetById(item.BookingId, HttpContext.RequestAborted);

        //            if (bookingDetail == null)
        //            {
        //                errItems += item.BookingId + "";
        //                continue;
        //            }

        //            //chECK Thêm số lượng nếu có

        //        }

        //        //Kiểm tra xem đã có khách hàng nào trùng thông tin chưa ?

        //        var customerInfor = await _bookingHotelUnitOfWork.CustomerRepository.GetById(requestData.customer.CusPhone, HttpContext.RequestAborted);

        //        if (customerInfor == null)
        //        {
        //            // TH1 : chưa có
        //            // tạo khách hàng để lấy CustomerId
        //            var cusID = await _bookingHotelUnitOfWork.CustomerRepository.Add(requestData.customer,HttpContext.RequestAborted);
        //            if (cusID != null)
        //            {
        //                returnData.ResponseCode = "-1";
        //                returnData.Description = "Dữ liệu không hợp lệ";
        //                return Ok(returnData);
        //            }

        //            customerId = cusID.Id;

        //        }
        //        else
        //        {
        //            // TH2 : đã có 
        //            // Lấy customer theo thông tin khách nhập

        //            customerId = customerInfor.Id;
        //        }


        //        foreach (var item in requestData.orderItems)
        //        {
        //            //var bookingDetail = new List<SystemsViewModel>();
        //            var bookingDetail = await _bookingHotelUnitOfWork.RoomsRepository.GetById(item.BookingId, HttpContext.RequestAborted);

        //            totalAmount += bookingDetail.Price;
        //        }
        //        // Tạo order 

        //        var order = new Booking
        //        {
        //            TotalAmount = totalAmount,
        //            CreatedDate = DateTime.Now,
        //            CustomerId = customerId,
        //        };

        //        var orderId = await _bookingHotelUnitOfWork.BookingsRepository.Add(order, HttpContext.RequestAborted);
        //        if (orderId != null)
        //        {
        //            // tạo order Detail
        //            foreach (var item in requestData.orderItems)
        //            {
        //                var rs = _bookingHotelUnitOfWork.BookingDetailsRepository.Add(new BookingDetail
        //                {
        //                    BookingId = item.BookingId,
        //                    Quantity = item.Quantity,
        //                    CreateAt = DateTime.Now
        //                }, HttpContext.RequestAborted);
        //            }
        //        }
        //        returnData.ResponseCode = "1"; ;
        //        returnData.Description = "Chúc mừng bạn đã tạo đơn hàng thành công !";
        //        return Ok(returnData);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

    }
}

