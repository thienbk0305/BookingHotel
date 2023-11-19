using BookingHotel.Models;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace BookingHotel.Controllers
{
    public class BookingController : Controller
    {
        // GET: BookingCart
        public IActionResult Index()
        {
            var list_cart = new List<BookingCart>();
            try
            {
                var cart = Request.Cookies["BookingCart"] != null ? Request.Cookies["BookingCart"]!.ToString() : string.Empty;
                list_cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookingCart>>(HttpUtility.UrlDecode(cart));
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(list_cart);
        }

        public JsonResult CheckOut(Customer customerReq)
        {
            var list_cart = new List<BookingCart>();
            var returnData = new ReturnData();
            var listBookingSend = new List<OrderRequest>();
            try
            {
                // Lấy thông tin giỏ hàng từ cookies
                var cart = Request.Cookies["BookingCart"] != null ? Request.Cookies["BookingCart"]!.ToString() : string.Empty;
                list_cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookingCart>>(HttpUtility.UrlDecode(cart));

                if (list_cart == null || list_cart.Count <= 0 || customerReq == null)
                {
                    returnData.ResponseCode = "-1";
                    returnData.Description = "Dữ liệu đầu vào không hợp lệ!";
                    return Json(returnData);
                }

                // tạo thông tin BookingId ,Quantity sẽ gửi lên Server
                foreach (var item in list_cart)
                {
                    listBookingSend.Add(new OrderRequest
                    {
                        BookingId = item.BookingId,
                        Quantity = item.Quantity
                    });
                }

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Bookings/OrderInsert";

                var req = new CreateOrderRequestData
                {
                    customer = customerReq,
                    orderItems = listBookingSend
                };
                var dataJson = JsonConvert.SerializeObject(req);

                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;

                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);
                if (!string.IsNullOrEmpty(result))
                {
                    returnData = JsonConvert.DeserializeObject<ReturnData>(result);
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
