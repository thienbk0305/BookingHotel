using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
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
                }).Where(m =>
                (m.Id != null && m.Id.ToLower().Contains(searchValue!.ToString().ToLower()) ||
                m.Email != null && m.Email.ToLower().Contains(searchValue!.ToString().ToLower()) ||
                m.PhoneNumber != null && m.PhoneNumber.ToLower().Contains(searchValue!.ToString().ToLower())) &&
                (m.RoleId != null && m.RoleId.ToLower().Contains(roleValue!.ToString().ToLower()))).AsNoTracking().ToListAsync();
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

        public async Task<int> UpdateAsync(ProfileView p, CancellationToken cancellation)
        {

            var pId = new SqlParameter("@p0", p.Id);
            var pFullName = new SqlParameter("@p1", p.FullName);
            pFullName.Value = String.IsNullOrEmpty(p.FullName) ? String.Empty : p.FullName;
            var pPhoneNumber = new SqlParameter("@p2", p.PhoneNumber);
            pPhoneNumber.Value = String.IsNullOrEmpty(p.PhoneNumber) ? String.Empty : p.PhoneNumber;
            var pEmail = new SqlParameter("@p3", p.Email);
            pEmail.Value = String.IsNullOrEmpty(p.Email) ? String.Empty : p.Email;
            var pUserName = new SqlParameter("@p4", p.UserName);
            pUserName.Value = String.IsNullOrEmpty(p.UserName) ? String.Empty : p.UserName;
            var pGender = new SqlParameter("@p5", p.Gender);
            var pNationId = new SqlParameter("@p6", p.NationId);
            pNationId.Value = String.IsNullOrEmpty(p.NationId) ? String.Empty : p.NationId;
            var pAddress = new SqlParameter("@p7", p.Address);
            pAddress.Value = String.IsNullOrEmpty(p.Address) ? String.Empty : p.Address;
            var pAvatarImage = new SqlParameter("@p8", p.AvatarImage);
            pAvatarImage.Value = String.IsNullOrEmpty(p.AvatarImage) ? String.Empty : p.AvatarImage;
            var pActive = new SqlParameter("@p9", p.Active);
            var pRoleId = new SqlParameter("@r0", p.RoleId);

            return await _db.Database.ExecuteSqlRawAsync("EXEC SP_UpdatePID @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@r0", pId, pFullName, pPhoneNumber, pEmail, pUserName, pGender, pNationId, pAddress, pAvatarImage, pActive, pRoleId);
        }
    }
}