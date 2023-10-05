using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
