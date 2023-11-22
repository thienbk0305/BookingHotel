using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.DBContext;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DataAccess.Repositories;


namespace BookingHotel.Controllers
{
    public class cusAccountController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly BookingHotelDbContext _dbContext;
        public cusAccountController(BookingHotelDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
           
        }
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public JsonResult Authen(LoginModel model)
		{
			var returnData = new ReturnData();
			try
			{
				// Kiểm tra thông tin từ Ajax gửi xuống
				if (model == null ||
					string.IsNullOrEmpty(model.Email)
					|| string.IsNullOrEmpty(model.Password))
				{
					returnData.ResponseCode = "-1";
					returnData.Description = "Vui lòng điền đầy đủ thông tin";
					return Json(returnData);
				}

				var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
				var base_url = "Authenticate/Login"; //API Controller
				var dataJson = JsonConvert.SerializeObject(model);
				var result = Common.HttpHelper.WebPost(RestSharp.Method.Post, url_api, base_url, dataJson);

				if (string.IsNullOrEmpty(result))
				{
					returnData.ResponseCode = "-1";
					returnData.Description = "Đăng nhập thất bại";
					return Json(returnData);
				}


				var tokenInfor = JsonConvert.DeserializeObject<TokenInfor>(result);
				if (tokenInfor != null && !string.IsNullOrEmpty(tokenInfor.token))
				{

					tokenInfor.ResponseCode = "1";
					tokenInfor.Description = "Đăng nhập thành công";
					tokenInfor.Extention = model.Email;

                    _contextAccessor.HttpContext!.Session.SetString("TOKEN_EMAIL", model.Email);
					if (string.IsNullOrEmpty(tokenInfor.Fullname))
					{
						tokenInfor.Fullname = "";
                    }
                    if (string.IsNullOrEmpty(tokenInfor.PhoneNumber))
                    {
                        tokenInfor.PhoneNumber = "";
                    }
                    _contextAccessor.HttpContext!.Session.SetString("TOKEN_FULLNAME", tokenInfor.Fullname!);
                    _contextAccessor.HttpContext!.Session.SetString("TOKEN_PHONENUMBER", tokenInfor.PhoneNumber!);
                }
				else
				{
					tokenInfor!.ResponseCode = "-1";
					tokenInfor.Description = "Đăng nhập thất bại";
				}
				// trả kết quả về View 
				return Json(tokenInfor);
			}
			catch (Exception)
			{

				throw;
			}

		}
		[HttpPost]
		public JsonResult Register(LoginModel model)
		{
			var returnData = new ReturnData();
			try
			{
				// Kiểm tra thông tin từ Ajax gửi xuống
				if (model == null ||
					string.IsNullOrEmpty(model.Email)
					|| string.IsNullOrEmpty(model.Password))
				{
					returnData.ResponseCode = "-1";
					returnData.Description = "Vui lòng điền đầy đủ thông tin";
					return Json(returnData);
				}

				var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
				var base_url = "Authenticate/Register"; //API Controller
				var dataJson = JsonConvert.SerializeObject(model);
				var result = Common.HttpHelper.WebPost(RestSharp.Method.Post, url_api, base_url, dataJson);

				if (string.IsNullOrEmpty(result))
				{
					returnData.ResponseCode = "-1";
					returnData.Description = "Đăng ký thất bại";
					return Json(returnData);
				}

				returnData.ResponseCode = "1";
				returnData.Description = "Đăng ký thành công";
				// trả kết quả về View 
				return Json(returnData);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}

