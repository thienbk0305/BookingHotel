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

namespace DataAccess.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private BookingHotelDbContext _db;

        public GenericRepository(BookingHotelDbContext db)
        {
            _db = db;
        }

        /// Add new entity
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        /// Delete an entity
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            T existing = _db.Set<T>().Find(entity);
            if (existing != null)
                _db.Set<T>().Remove(existing);
        }

        /// Delete entities
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
        }

        /// Get entity by id
        /// <param name="id"></param>
        public T GetById(int id, bool allowTracking = true)
        {
            return _db.Set<T>().FirstOrDefault(c => ((int)c.GetType().GetProperty("Id").GetValue(c) == id));
        }

        /// Get entity by lambda expression
        /// <param name="predicate"></param>
        public T Get(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return _db.Set<T>().FirstOrDefault(predicate);
        }

        /// Get list of entities
        public IEnumerable<T> GetAll(bool allowTracking = true)
        {
            return _db.Set<T>().AsEnumerable();
        }

        /// Get entites by lambda expression
        /// <param name="predicate"></param>
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return _db.Set<T>().Where(predicate).AsEnumerable();
        }

        /// Update an entity
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.Set<T>().Attach(entity);
        }

        /// Get entities from sql string
        /// <param name="sql"></param>
        public IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true)
        {
            if (allowTracking)
            {
                return _db.Set<T>().FromSqlRaw(sql).AsEnumerable();
            }

            return _db.Set<T>().FromSqlRaw(sql).AsEnumerable();
        }

        /// Get all entities async
        public async Task<IEnumerable<T>> GetAllAsync(bool allowTracking = true)
        {
            var data = await _db.Set<T>().ToListAsync();
            return data;
        }

        /// Get entities by lambda expression
        /// <param name="predicate"></param>
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate,
            bool allowTracking = true)
        {
            var data = await _db.Set<T>().Where(predicate).ToListAsync();
            return data;
        }

        /// Get entity by id async
        /// <param name="id"></param>
        public async Task<T> GetByIdAsync(int id, bool allowTracking = true)
        {
            var data = await _db.Set<T>().FirstOrDefaultAsync(c => ((int)c.GetType().GetProperty("Id").GetValue(c) == id));
            return data!;
        }

        /// Get entities by lambda expression
        /// <param name="predicate"></param>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            T data;

            if (allowTracking)
            {
                data = await _db.Set<T>().FirstOrDefaultAsync(predicate);
            }
            else
            {
                data = await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            return data!;
        }

        /// Get entities from sql string async
        /// <param name="sql"></param>
        public async Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true)
        {
            IEnumerable<T> data;
            data = await _db.Set<T>().FromSqlRaw(sql).ToListAsync();
            //if (allowTracking)
            //{
            //    data = await _db.Set<T>().FromSqlRaw(sql).ToListAsync();
            //}
            //else
            //{
            //    data = await _db.Set<T>().AsNoTracking().FromSql(sql).ToListAsync();
            //}
            return data;
        }
    }
}