using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.IRepositories;
using static DataAccess.Models.Permissions;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,IEntity
    {
        private BookingHotelDbContext _db;
        public GenericRepository(BookingHotelDbContext db)
        {
            _db = db;
        }

        public async Task<T> Add(T entity, CancellationToken cancellation)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity;
        }
        public async Task<T> Delete(int id, CancellationToken cancellation)
        {
            var entity = await _db.Set<T>().AsAsyncEnumerable().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                return entity!;
            }
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity!;
        }

        public async Task<List<T>> GetAll(CancellationToken cancellation)
        {
            var entity = await _db.Set<T>().AsAsyncEnumerable().ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task<T> GetById(int id, CancellationToken cancellation)
        {
            var entity = await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            await _db.SaveChangesAsync(cancellation);
            return entity!;
        }

        public async Task<T> Update(string userId, CancellationToken cancellation)
        {

            var entity = await _db.Set<T>().AsAsyncEnumerable().FirstOrDefaultAsync(e => e.UserId == userId);
            if (entity == null)
            {
                return entity!;
            }
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync(cancellation);
            return entity!;
        }
    }
}