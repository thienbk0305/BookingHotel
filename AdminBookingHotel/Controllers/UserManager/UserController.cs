using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using DataAccess.Models;
using Newtonsoft.Json;
using APIBookingHotel.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AdminBookingHotel.Controllers.User
{
    public class UserController : Controller
    {
        IUtilitiesRepository<ProfileView> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookingHotelDbContext _dbContext;
        public UserController(IUtilitiesRepository<ProfileView> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration, RoleManager<IdentityRole> roleManager, BookingHotelDbContext dbContext)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;
            _roleManager = roleManager;
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
                var roleValue = Request.Form["group"].ToString();

                var listResult = new List<ProfileView>();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Identity/Users?searchValue=" + searchValue + "&roleValue=" + roleValue; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result) || result == null)
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index","Home");
                }

                listResult = JsonConvert.DeserializeObject<List<ProfileView>>(result);

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
                return NotFound();
            }
            var listResult = new ProfileViewById();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Identity/GetUser/" + id + ""; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "User");
            }

            listResult = JsonConvert.DeserializeObject<ProfileViewById>(result);
            var roleNames = _dbContext.Roles.ToList();
            ViewData["RoleName"] = new SelectList(roleNames, "Id", "Name");
            return PartialView("Detail", listResult!.Data);
        }
        [HttpPost]
        public IActionResult Update(string id, ProfileView model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var listResult = new ProfileViewById();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Identity/UpdateUser"; //API Controller
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Put, url_api, base_url, dataJson, token);
                if (string.IsNullOrEmpty(result))
                {
                    _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                    return Json(new { result = "Redirect", url = Url.Action("Detail", "User", new { id = id  }) });
                }

            }
            _toastNotification.AddSuccessToastMessage("Cập nhật thông tin User thành công!");
            return RedirectToAction("Index");

        }
    }
}
