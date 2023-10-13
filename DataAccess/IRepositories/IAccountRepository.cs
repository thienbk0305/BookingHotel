using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<User> LoginModelAsync(LoginModel model);
    }

}
