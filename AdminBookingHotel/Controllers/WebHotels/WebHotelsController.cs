using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.HotelsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AdminBookingHotel.Controllers.WebHotels
{
    public class WebHotelsController : Controller
    {
        IUtilitiesRepository<HotelsViewModel> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;

        private readonly IConfiguration _configuration;

        public WebHotelsController(IUtilitiesRepository<HotelsViewModel> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;

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

                var listResult = new List<HotelsViewModel>();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Hotels/GetHotels"; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result) || result == null)
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index");
                }

                listResult = JsonConvert.DeserializeObject<List<HotelsViewModel>>(result);

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
            var Hotels = new HotelsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Hotels/SingleHotels?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(Hotels);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebHotels");
            }

            Hotels = JsonConvert.DeserializeObject<HotelsViewModel>(result);

            return PartialView("_Detail", Hotels);
        }

        [HttpPost]
        public IActionResult Update([FromBody] HotelsViewModel model)
        {
            var id = model.Id;
            try
            {

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "";
                if (string.IsNullOrEmpty(id) || id == "0")
                {
                    base_url = "Hotels/AddHotels"; //API Controller
                } else
                {
                    base_url = "Hotels/EditHotels?id=" + model.Id; //API Controller
                }
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var updatedHotels = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(updatedHotels))
                {
                    return RedirectToAction("Index", "WebHotels");
                }

                var result = JsonConvert.DeserializeObject<RoleResult>(updatedHotels);
                _toastNotification.AddSuccessToastMessage("Success!");
                return RedirectToAction("Index", "WebHotels");

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
            var base_url = "Hotels/DeleteHotels?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(id);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var deleteHotels = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Delete, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(deleteHotels))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebHotels");
            }

            var result = JsonConvert.DeserializeObject<RoleResult>(deleteHotels);
            _toastNotification.AddSuccessToastMessage("Success!");
            return RedirectToAction("Index", "WebHotels");


        }
    }
}
