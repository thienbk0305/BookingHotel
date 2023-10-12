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
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var url_api = "https://localhost:7219/api/";
            var base_url = "Accounts/Login";
            var req = new LoginModel { Email = model.Email, Password = model.Password };
            var dataJson = JsonConvert.SerializeObject(req);
            var result = Common.HttpHelper.WebPost(url_api, base_url, dataJson);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Invalid email or password.");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
