using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> RegisterAsync(RegisterModel model); //sử dụng IdentityResult để tạo xác thực user
        public Task<string> LoginModelAsync(LoginModel model);
    }

}
