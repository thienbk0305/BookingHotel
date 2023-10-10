using DataAccess.Models;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        public AccountsController(IAccountRepository repo)
        {
            accountRepo = repo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var result = await accountRepo.RegisterAsync(registerModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await accountRepo.LoginModelAsync(loginModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
