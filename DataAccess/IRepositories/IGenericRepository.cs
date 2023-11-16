using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.IRepositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll(CancellationToken cancellation);
        Task<T> GetById(string id, CancellationToken cancellation);
        Task<T> Add(T entity, CancellationToken cancellation);
        Task<T> Update(T entity, CancellationToken cancellation);
        Task<T> Delete(string id, CancellationToken cancellation);
        Task<IEnumerable<T>> FromSqlQueryAsync(string sql, CancellationToken cancellation);
    }
}