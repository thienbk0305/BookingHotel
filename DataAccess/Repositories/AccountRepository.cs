using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Repositories
{
    public class AccountRepository : GenericRepository<DataAccess.Entities.User>, IAccountRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        BookingHotelDbContext _dbContext;

        public AccountRepository(BookingHotelDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : base(dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._dbContext = dbContext;
        }
        public async Task<User> LoginModelAsync(LoginModel model)
        {
            try
            {
                // Login : tìm xem có username và passwork nào trong db giống client truyền lên không
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                // nếu không có thì trả về acc rỗng
                if (!result.Succeeded)
                {
                    return new User();
                }
                var user = _dbContext.User.ToList().FindAll(s => s.Email == model.Email).FirstOrDefault(); ;

                return user!;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            return await userManager.CreateAsync(user, model.Password);
        }
    }
}
