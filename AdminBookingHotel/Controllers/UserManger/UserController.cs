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
        private readonly BookingHotelDbContext _dbContext;
        public UserController(IUtilitiesRepository<ProfileView> utilitiesRepository, 
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
                var base_url = "Identity/Role/Users?searchValue="+ searchValue + "&roleValue="+ roleValue; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result))
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index");
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

        //[HttpGet]
        //public async Task<IActionResult> Detail(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await _dbContext.User.FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
        //        return RedirectToAction("Index", "User");
        //    }
        //    return View(user);
        //}
    }
}
