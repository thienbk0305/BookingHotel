using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
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
        //[Route("OrderInsert")]
        //[AllowAnonymous]
        //public async Task<ActionResult> OrderInsert(CreateOrderRequestData requestData)
        //{
        //    var returnData = new ReturnData();
        //    try
        //    {

        //        if (requestData == null)
        //        {
        //            returnData.ResponseCode = "-1";
        //            returnData.Description = "Dữ liệu không hợp lệ";
        //            return Ok(returnData);
        //        }

        //        var errItems = "";
        //        var totalAmount = 0;
        //        // Kiểm tra xem giá tiền của Booking có khớp với DB không?
        //        foreach (var item in requestData.orderItems)
        //        {
        //            var bookingDetail= await _bookingHotelUnitOfWork.RoomsRepository.GetById(item.BookingId, HttpContext.RequestAborted);
        //            var bookingDetail = _context.sanpham.ToList().Where(s => s.PrductID == item.ProductID).FirstOrDefault();
        //            if (bookingDetail == null || bookingDetail.BookingId <= 0)
        //            {
        //                errItems += item.ProductID + "";
        //                continue;
        //            }

        //            //chECK Thêm số lượng nếu có

        //        }

        //        var customerId = 0;
        //        //Kiểm tra xem đã có khách hàng nào trùng thông tin chưa ?

        //        var customerInfor = _context.customer.
        //            Where(s => s.CustomerPhoneNumber == requestData.customer.CustomerPhoneNumber).FirstOrDefault();

        //        if (customerInfor == null || customerInfor.CustomerID <= 0)
        //        {
        //            // TH1 : chưa có
        //            // tạo khách hàng để lấy CustomerId
        //            var cusID = await _orderRepository.Customer_Insert(requestData.customer);

        //            if (cusID <= 0)
        //            {
        //                returnData.ResponseCode = "-1";
        //                returnData.Description = "Dữ liệu không hợp lệ";
        //                return Ok(returnData);
        //            }

        //            customerId = cusID;

        //        }
        //        else
        //        {
        //            // TH2 : đã có 
        //            // Lấy customer theo thông tin khách nhập
        //            customerId = customerInfor.CustomerID;
        //        }


        //        foreach (var item in requestData.orderItems)
        //        {
        //            var productDetail = _context.sanpham.ToList().Where(s => s.PrductID == item.ProductID).FirstOrDefault();

        //            totalAmount += productDetail.DonGia;
        //        }
        //        // Tạo order 



        //        var order = new Order
        //        {
        //            TotalAmount = totalAmount,
        //            CreatedDate = DateTime.Now,
        //            CustomerID = customerId,
        //        };

        //        var orderID = await _orderRepository.OrderInsert(order);
        //        if (orderID > 0)
        //        {
        //            // tạo order Detail
        //            foreach (var item in requestData.orderItems)
        //            {
        //                var rs = _orderRepository.OrderDetailInsert(new OrderDetail
        //                {
        //                    OrderID = orderID,
        //                    ProductID = item.ProductID,
        //                    Quantity = item.Quantity,
        //                    CreateAt = DateTime.Now
        //                });
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

