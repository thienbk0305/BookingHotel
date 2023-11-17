using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("news")]
    public class NewController : Controller
    {
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("detail")]
        [Route("")]
        public IActionResult NewDetail() { 
            return View();
        }

        [Route("aboutUs")]
        [Route("")]
        public IActionResult AboutUs()
        {
            return View();
        }

    }

}
