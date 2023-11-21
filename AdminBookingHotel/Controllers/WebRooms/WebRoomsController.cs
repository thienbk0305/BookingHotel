using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.RoomsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AdminBookingHotel.Controllers.WebRooms
{
    public class WebRoomsController : Controller
    {
        IUtilitiesRepository<RoomsViewModel> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;

        private readonly IConfiguration _configuration;

        public WebRoomsController(IUtilitiesRepository<RoomsViewModel> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;

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

                var listResult = new List<RoomsViewModel>();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Rooms/GetRooms"; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result) || result == null)
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index");
                }

                listResult = JsonConvert.DeserializeObject<List<RoomsViewModel>>(result);

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
            var rooms = new RoomsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Rooms/SingleRooms?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(rooms);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebRooms");
            }

            rooms = JsonConvert.DeserializeObject<RoomsViewModel>(result);

            return PartialView("_Detail", rooms);
        }

        [HttpPost]
        public IActionResult Update([FromBody] RoomsViewModel model)
        {
            var id = model.Id;
            try
            {

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "";
                if (string.IsNullOrEmpty(id) || id == "0")
                {
                    base_url = "Rooms/AddRooms"; //API Controller
                } else
                {
                    base_url = "Rooms/EditRooms?id=" + model.Id; //API Controller
                }
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var updatedRooms = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(updatedRooms))
                {
                    return RedirectToAction("Index", "WebRooms");
                }

                var result = JsonConvert.DeserializeObject<RoleResult>(updatedRooms);
                _toastNotification.AddSuccessToastMessage("Success!");
                return RedirectToAction("Index", "WebRooms");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return PartialView("_Detail");
            }

            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Rooms/DeleteRooms?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(id);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var deleteRooms = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Delete, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(deleteRooms))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebRooms");
            }

            var result = JsonConvert.DeserializeObject<RoleResult>(deleteRooms);
            _toastNotification.AddSuccessToastMessage("Success!");
            return RedirectToAction("Index", "WebRooms");


        }
    }
}
