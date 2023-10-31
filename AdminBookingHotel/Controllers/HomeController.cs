using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminBookingHotel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("TOKEN_SERVER");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

    }
}