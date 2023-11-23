using DataAccess.Models.SystemsModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HotelDetail(string id)
        {
            var hotel = new List<SystemsViewModel>();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Systems/GetSystemsByHotelId?hotelId=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(hotel);
            var result = Common.HttpHelper.WebPost(RestSharp.Method.Get, url_api, base_url, dataJson);
            hotel = JsonConvert.DeserializeObject<List<SystemsViewModel>>(result);
            return View(hotel);
        }
        public IActionResult HotelFilter(string searchString)
        {
            var hotel = new List<SystemsViewModel>();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Systems/GetSystems?searchValue=" + searchString; //API Controller
            var dataJson = JsonConvert.SerializeObject(hotel);
            var result = Common.HttpHelper.WebPost(RestSharp.Method.Get, url_api, base_url, dataJson);
            hotel = JsonConvert.DeserializeObject<List<SystemsViewModel>>(result);
            return View(hotel);
        }
    }
}