using DataAccess.Models.NewsModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingHotel.Controllers
{
    [Route("news")]
    public class NewController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var listResult = new List<NewsViewModel>();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "news/getnews"; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            
            var result = Common.HttpHelper.WebPost(RestSharp.Method.Get, url_api, base_url, dataJson);
            listResult = JsonConvert.DeserializeObject<List<NewsViewModel>>(result);
            return View(listResult);
        }

        [Route("detail")]
        public IActionResult Detail(string id) 
        {
             if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            var news = new NewsViewModel();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "News/SingleNews?id=" + id; //API Controller
            var dataJson = JsonConvert.SerializeObject(news);
            var result = Common.HttpHelper.WebPost(RestSharp.Method.Get, url_api, base_url, dataJson);


            news = JsonConvert.DeserializeObject<NewsViewModel>(result);
            return View(news);
        }

    }

}
