using AdminBookingHotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminBookingHotel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}