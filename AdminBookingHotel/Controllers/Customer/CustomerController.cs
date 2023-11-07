using Microsoft.AspNetCore.Mvc;

namespace AdminBookingHotel.Controllers.Customer
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
