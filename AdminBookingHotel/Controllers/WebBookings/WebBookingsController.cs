using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.BookingsModels;
using DataAccess.Models.SystemsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AdminBookingHotel.Controllers.WebSystems
{
    public class WebBookingsController : Controller
    {
        IUtilitiesRepository<BookingsViewModel> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly BookingHotelDbContext _dbContext;

        public WebBookingsController(IUtilitiesRepository<BookingsViewModel> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration, BookingHotelDbContext dbContext)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("TOKEN_SERVER");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        [HttpPost]
        public IActionResult LoadData()
        {
            try
            {
                var draw = (HttpContext.Request.Form["draw"].FirstOrDefault() == null
                     ? "1"
                     : HttpContext.Request.Form["draw"].FirstOrDefault());
                var start = Request.Form["start"].FirstOrDefault();
                var length = (Request.Form["length"].FirstOrDefault() == null
                        ? "10"
                        : Request.Form["length"].FirstOrDefault());
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                var listResult = new List<BookingsViewModel>();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Systems/GetBookings?searchValue=" + searchValue; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result) || result == null)
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index");
                }

                listResult = JsonConvert.DeserializeObject<List<BookingsViewModel>>(result);

                var returnedData = _utilitiesRepository.InitiateDataTable(draw!, length!, start!, listResult!);
                return returnedData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {

            if (id == null)
            {
                return PartialView("_Detail");
            }
            var Systems = new BookingsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Systems/SingleBookings?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(Systems);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebBookings");
            }

            Systems = JsonConvert.DeserializeObject<BookingsViewModel>(result);

            return PartialView("_Detail", Systems);
        }

        [HttpPost]
        public IActionResult Update([FromBody] BookingsViewModel model)
        {
            var id = model.Id;
            try
            {

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Systems/EditBookings?id=" + model.Id; //API Controller

                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var updatedSystems = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(updatedSystems))
                {
                    return RedirectToAction("Index", "WebBookings");
                }

                var result = JsonConvert.DeserializeObject<RoleResult>(updatedSystems);
                _toastNotification.AddSuccessToastMessage("Success!");
                return RedirectToAction("Index", "WebBookings");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
