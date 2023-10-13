using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.DBContext;
using Newtonsoft.Json;
using System.Text;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace AdminBookingHotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookingHotelDbContext _dbContext;
        public AccountController(BookingHotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
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
                // Nếu pass kiểm trả thì goi sang API Net Core
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Accounts/Login";
                var req = new LoginModel { Email = model.Email, Password = model.Password };
                var dataJson = JsonConvert.SerializeObject(req);
                var result = Common.HttpHelper.WebPost(url_api, base_url, dataJson);
                // Nhận kết quả trả về 

                if (string.IsNullOrEmpty(result))
                {
                    returnData.ResponseCode = "-1";
                    returnData.Description = "Đăng nhập thất bại";
                    return Json(returnData);
                }

                returnData = JsonConvert.DeserializeObject<ReturnData>(result);

                //if (returnData.ResponseCode != null)
                //{

                //    Session["USER_ID"] = returnData.ResponseCode;
                //    Session["USER_FULLNAME"] = returnData.Extention;
                //}
                // trả kết quả về View 
                //return Json(returnData);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
