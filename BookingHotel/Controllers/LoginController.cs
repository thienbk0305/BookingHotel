using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("admin/login")]
    public class LoginController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }
    }
}
