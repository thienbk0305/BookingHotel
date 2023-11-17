using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("hotel")]
    public class HotelController : Controller
    {
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        //hotel detail page
        [Route("hoteldetail")]
        [Route("")]
        public IActionResult hotelDetail()
        {
            return View();
        }

        [Route("hotel-filter")]
        [Route("")]
        public IActionResult hotelFilter() { 
            return View();
        }
    }
}
