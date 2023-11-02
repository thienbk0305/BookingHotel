using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.IRepositories
{
    public interface IGenericRepository<T> where T : class,IEntity
    {
        Task<List<T>> GetAll(CancellationToken cancellation);
        Task<T> GetById(int id, CancellationToken cancellation);
        Task<T> Add(T entity, CancellationToken cancellation);
        Task<T> Update(string userId, CancellationToken cancellation);
        Task<T> Delete(int id, CancellationToken cancellation);
    }
}