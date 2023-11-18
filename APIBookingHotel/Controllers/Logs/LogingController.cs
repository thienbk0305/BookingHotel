using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using APIBookingHotel.LogServices;
using APIBookingHotel.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIBookingHotel.Controllers.Logs
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogingController : ControllerBase
    {
        public ILogger<Checklogs> _logger;
        private readonly ILoggerManager _loggerManager;

        public LogingController(ILogger<Checklogs> logger, ILoggerManager loggerManager)
        {
            _logger = logger;
            _loggerManager = loggerManager;
        }

        [HttpPost("Index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var logID = DateTime.Now.Ticks;
            var Checklogs = new Checklogs("thienbk", "BookingHotel", _logger);

            _loggerManager.LogInfo(logID + " | user:" + JsonConvert.SerializeObject(Checklogs));
            _loggerManager.LogDebug("Here is debug message from the controller.");
            _loggerManager.LogWarn("Here is warn message from the controller.");
            _loggerManager.LogError("Here is error message from the controller.");

            return Ok();
        }

    }
}
