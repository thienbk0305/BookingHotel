using BookingHotel.Models;
using DataAccess.Models.HotelsModels;
using DataAccess.Models.SystemsModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookingHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var listResult = new List<SystemsViewModel>();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Systems/GetSystems"; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            var result = Common.HttpHelper.WebPost(RestSharp.Method.Get, url_api, base_url, dataJson);
            if (result != null)
            {
                listResult = JsonConvert.DeserializeObject<List<SystemsViewModel>>(result);
                listResult = listResult.Where(h => h.Status == DataAccess.Entities.Status.Status_2).ToList();
            }

            return View(listResult);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}