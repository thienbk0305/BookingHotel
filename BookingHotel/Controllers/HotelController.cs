using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HotelDetail()
        {
            return View();
        }
        public IActionResult HotelFilter()
        {
            return View();
        }
    }
}
