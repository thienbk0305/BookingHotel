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
    public class GenericRepository<TContext, T> : IGenericRepository<T>
    where T : class, IEntity where TContext : DbContext
    {
        protected readonly TContext _context;
        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity, CancellationToken cancellation)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync(cancellation);
            return entity;
        }
        public async Task<T> Delete(string id, CancellationToken cancellation)
        {
            var entity = await _context.Set<T>().AsAsyncEnumerable().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                return entity!;
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellation);
            return entity!;
        }

        public async Task<List<T>> GetAll(CancellationToken cancellation)
        {
            var entity = await _context.Set<T>().AsAsyncEnumerable().ToListAsync();
            await _context.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task<T> GetById(string id, CancellationToken cancellation)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            await _context.SaveChangesAsync(cancellation);
            return entity!;
        }

        public async Task<T> Update(T entity, CancellationToken cancellation)
        {

            if (entity == null)
            {
                return entity!;
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellation);
            return entity!;
        }

        /// <summary>
        /// Get entities from sql string async
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FromSqlQueryAsync(string sql, CancellationToken cancellation)
        {
            IEnumerable<T> data;

            data = await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
            await _context.SaveChangesAsync(cancellation);
            return data;
        }
    }
}