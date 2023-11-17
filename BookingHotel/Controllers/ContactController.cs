using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace BookingHotel.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
	{

        IUtilitiesRepository<ContactView> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookingHotelDbContext _dbContext;
        public ContactController(IUtilitiesRepository<ContactView> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration, RoleManager<IdentityRole> roleManager, BookingHotelDbContext dbContext)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;
            _roleManager = roleManager;
            _dbContext = dbContext;

        }

        [Route("contact")]
        [Route("")]
        public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Insert(ContactView model)
        {

            if (ModelState.IsValid)
            {   
                var listResult = new ContactView();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Customer/Customer/AddContact"; //API Controller
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Put, url_api, base_url, dataJson, token);
                if (string.IsNullOrEmpty(result))
                {
                    _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại thông tin");
                    return RedirectToAction("Index");
                }

            }
            _toastNotification.AddSuccessToastMessage("Cập nhật thông tin User thành công!");
            return RedirectToAction("Index");

        }



        [Route("partnerForm")]
        [Route("")]
        public IActionResult partnerForm() {
            return View();
        }


    }
}
