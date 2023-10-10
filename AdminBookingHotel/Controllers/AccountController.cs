using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.DBContext;
using Newtonsoft.Json;
using System.Text;

namespace AdminBookingHotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookingHotelDbContext _dbContext;
        public AccountController(BookingHotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var _user = _dbContext.User.Where(m => m.Email == model.Email && m.PasswordHash == model.Password);
        //    if (_user == null)
        //    {
        //        return BadRequest("Invalid email or password.");
        //    }

        //    return RedirectToAction("Index","Account");
        //}
    }
}
