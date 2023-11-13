using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.NewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AdminBookingHotel.Controllers.News
{
    public class WebNewsController : Controller
    {
        IUtilitiesRepository<NewsViewModel> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;

        private readonly IConfiguration _configuration;

        public WebNewsController(IUtilitiesRepository<NewsViewModel> utilitiesRepository,
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

                var listResult = new List<NewsViewModel>();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "news/getnews"; //API Controller
                var dataJson = JsonConvert.SerializeObject(listResult);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result) || result == null)
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index");
                }

                listResult = JsonConvert.DeserializeObject<List<NewsViewModel>>(result);

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
            var news = new NewsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "News/SingleNews?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(news);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebNews");
            }

            news = JsonConvert.DeserializeObject<NewsViewModel>(result);

            return PartialView("_Detail", news);
        }

        [HttpPost]
        public IActionResult Update([FromBody] NewsViewModel model)
        {
            var id = model.Id;
            model.NewsContent = WebUtility.HtmlEncode(model.NewsContent);
            try
            {

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "";
                if (string.IsNullOrEmpty(id) || id == "0")
                {
                    base_url = "News/AddNews"; //API Controller
                } else
                {
                    base_url = "News/EditNews?id=" + model.Id; //API Controller
                }
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var updatedNews = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(updatedNews))
                {
                    return RedirectToAction("Index", "WebNews");
                }

                var result = JsonConvert.DeserializeObject<RoleResult>(updatedNews);
                _toastNotification.AddSuccessToastMessage("Success!");
                return RedirectToAction("Index", "WebNews");

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
            var news = new NewsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "News/DeleteNews?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(news);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Delete, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                return RedirectToAction("Index", "WebNews");
            }

            news = JsonConvert.DeserializeObject<NewsViewModel>(result);
            _toastNotification.AddSuccessToastMessage("Success!");
            return RedirectToAction("Index", "WebNews");


        }
    }
}
