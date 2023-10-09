using APIBookingHotel.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> RegisterAsync(RegisterModel model);
        public Task<string> LoginModelAsync(LoginModel model);
    }

}
