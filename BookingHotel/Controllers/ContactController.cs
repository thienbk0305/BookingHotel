using DataAccess.DBContext;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Models.CustomersModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace BookingHotel.Controllers
{

    public class ContactController : Controller
	{

        IUtilitiesRepository<CustomersViewModel> _utilitiesRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookingHotelDbContext _dbContext;
        public ContactController(IUtilitiesRepository<CustomersViewModel> utilitiesRepository,
                              IToastNotification toastNotification, IConfiguration configuration, RoleManager<IdentityRole> roleManager, BookingHotelDbContext dbContext)
        {
            _utilitiesRepository = utilitiesRepository;
            _toastNotification = toastNotification;
            _configuration = configuration;
            _roleManager = roleManager;
            _dbContext = dbContext;

        }
        public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Insert(CustomersViewModel model)
        {
            try
            {

                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Customers/AddContact"; //API Controller

                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var updatedCustomers = "";
                if (!string.IsNullOrEmpty(token))
                {
                     updatedCustomers = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Put, url_api, base_url, dataJson, token);
                }
                else
                {
                     updatedCustomers = Common.HttpHelper.WebPost(RestSharp.Method.Put, url_api, base_url, dataJson);
                }

                if (string.IsNullOrEmpty(updatedCustomers))
                {
                    return RedirectToAction("Index", "Contact");
                }

                //var result = JsonConvert.DeserializeObject<RoleResult>(updatedCustomers);
                _toastNotification.AddSuccessToastMessage("Success!");
                return RedirectToAction("Index", "Contact");

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
