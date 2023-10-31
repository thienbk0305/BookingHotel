using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
	[Route("contact")]
	public class ContactController : Controller
	{
		[Route("index")]
		[Route("")]
		public IActionResult Index()
		{
			return View();
		}


		//form đăng ký đối tác
        [Route("partner-form")]
        [Route("")]
        public IActionResult PartnerForm() { 
			return View();
		}
	}
}
