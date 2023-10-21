using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository: IGenericRepository<DataAccess.Entities.User>
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<User> LoginModelAsync(LoginModel model);
    }

}
