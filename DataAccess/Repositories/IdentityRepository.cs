using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static DataAccess.Models.Permissions;

namespace DataAccess.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly BookingHotelDbContext _db;

        public IdentityRepository(BookingHotelDbContext context)
        {
            _db = context;
        }

        /// Get all entities async <summary>
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="roleValue"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProfileView>> GetAllUserInRoleAsync(string searchValue, string roleValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }
            if (roleValue == null)
            {
                roleValue = "";
            }
            var data = await 
                (from users in _db.User
                join userRoles in _db.UserRoles on users.Id equals userRoles.UserId
                join roles in _db.Roles on userRoles.RoleId equals roles.Id
                select new ProfileView()
                {
                                  Id = users.Id,
                                  FullName = users.FullName,
                                  UserName = users.UserName,
                                  PhoneNumber = users.PhoneNumber,
                                  Email = users.Email,
                                  NationId = users.NationId,
                                  Active = users.Active,
                                  AvatarImage = users.AvatarImage,
                                  RoleId = roles.Id,
                                  RoleName = roles.Name
                }).Where(m =>
                (m.Id != null && m.Id.ToLower().Contains(searchValue!.ToString().ToLower()) ||
                m.Email != null && m.Email.ToLower().Contains(searchValue!.ToString().ToLower())) &&
                (m.RoleName != null && m.RoleName.ToLower().Contains(roleValue!.ToString().ToLower()))).AsNoTracking().ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return data;

        }

        public async Task<User> Add(User entity, CancellationToken cancellation)
        {
            await _db.Set<User>().AddAsync(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task<User> Delete(string id, CancellationToken cancellation)
        {
            var entity = await _db.Set<User>().AsAsyncEnumerable().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                return entity!;
            }
            _db.Set<User>().Remove(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity!;
        }

        public async Task<List<User>> GetAll(CancellationToken cancellation)
        {
            var entity = await _db.Set<User>().AsAsyncEnumerable().ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task<ProfileView> GetById(string id, CancellationToken cancellation)
        {
            var data = await
                (from users in _db.User
                 join userRoles in _db.UserRoles on users.Id equals userRoles.UserId
                 join roles in _db.Roles on userRoles.RoleId equals roles.Id
                 select new ProfileView()
                 {
                     Id = users.Id,
                     FullName = users.FullName,
                     UserName = users.UserName,
                     PhoneNumber = users.PhoneNumber,
                     Email = users.Email,
                     NationId = users.NationId,
                     Active = users.Active,
                     AvatarImage = users.AvatarImage,
                     RoleId = roles.Id,
                     RoleName = roles.Name
                 }).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            await _db.SaveChangesAsync(cancellation);
            return data!;
        }

        public async Task<User> Update(string userId, CancellationToken cancellation)
        {
            var entity = await _db.Set<User>().AsAsyncEnumerable().FirstOrDefaultAsync(e => e.Id == userId);
            if (entity == null)
            {
                return entity!;
            }
            _db.Set<User>().Update(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity!;
        }
    }
}