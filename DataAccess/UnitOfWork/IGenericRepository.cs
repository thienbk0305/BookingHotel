using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWork
{
    public interface IGenericRepository<T> where T : class
    {
        #region Sync Methods
        /// Get all entities

        IEnumerable<T> GetAll(bool allowTracking = true);

        /// Get entities by lambda expression
        /// <param name="predicate"></param>

        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool allowTracking = true);

        /// Get entity by id
        /// <param name="id"></param>

        T GetById(int id, bool allowTracking = true);

        /// Get entity by lambda expression
        /// <param name="predicate"></param>

        T Get(Expression<Func<T, bool>> predicate, bool allowTracking = true);

        /// Get entities from sql string
        /// <param name="query"></param>

        IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true);

        /// Add new antity
        /// <param name="entity"></param>
        void Add(T entity);

        /// Delete an entity
        /// <param name="entity"></param>
        void Delete(T entity);

        /// Delete the entities
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<T> entities);

        /// Update an entity
        /// <param name="entity"></param>
        void Update(T entity);

        #endregion

        #region Async Methods
        /// Get all entities async

        Task<IEnumerable<T>> GetAllAsync(bool allowTracking = true);

        /// Get entities lambda expression async
        /// <param name="predicate"></param>

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true);

        /// Get entity by id async
        /// <param name="id"></param>

        Task<T> GetByIdAsync(int id, bool allowTracking = true);

        /// Get entity by lambda expression
        /// <param name="predicate"></param>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true);

        /// Get entities from sql string async
        /// <param name="query"></param>
        Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true);

        #endregion
    }
}