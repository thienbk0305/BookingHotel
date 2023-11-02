using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<ProfileView>> GetAllUserInRoleAsync(string searchValue, string roleValue, CancellationToken cancellation);
        Task<List<DataAccess.Entities.User>> GetAll(CancellationToken cancellation);
        Task<ProfileView> GetById(string id, CancellationToken cancellation);
        Task<DataAccess.Entities.User> Add(DataAccess.Entities.User entity, CancellationToken cancellation);
        Task<DataAccess.Entities.User> Update(string userId, CancellationToken cancellation);
        Task<DataAccess.Entities.User> Delete(string id, CancellationToken cancellation);

    }
}