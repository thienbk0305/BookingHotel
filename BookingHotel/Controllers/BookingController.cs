using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("booking")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("bookingConfirm")]
        [Route("")]
        public IActionResult confirm()
        {
            return View();
        }

    }
}
