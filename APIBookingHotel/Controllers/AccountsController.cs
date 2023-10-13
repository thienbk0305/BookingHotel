using DataAccess.Models;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Entities;
using DataAccess.UnitOfWork;

namespace APIBookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IBookingHotelUnitOfWork _unitOfWork;
        private readonly IAccountRepository accountRepo;
        private IConfiguration _configuration;
        public AccountsController(IAccountRepository repo, IConfiguration configuration,IBookingHotelUnitOfWork unitOfWork)
        {
            accountRepo = repo;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
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
            var returnData = new ReturnData();
            try
            {
                // bước 1: login để lấy acc theo username và password truyền vào
                var user = await _unitOfWork.AccountRepository.LoginModelAsync(loginModel);

                if (user == null || user.Id == null)
                {
                    returnData.ResponseCode = "";
                    returnData.Description = "Đăng nhập thất bại";
                    return Ok(returnData);
                }

                // bước 2 : Tạo token dựa trên object user
                var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 };

                var newAccessToken = CreateToken(authClaims);

                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

                returnData.ResponseCode = user.Id;
                returnData.Extention = user.UserName;
                returnData.Description = token;

                return Ok(returnData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
