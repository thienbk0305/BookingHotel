using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
