using System.Linq.Expressions;
using MongoDB.Driver;
using WhatsInt.Infrastructure.Entities.Generic;

namespace Infrastructure.Repository
{
    public interface IRepository<T, in TKey> : IQueryable<T> where T : IEntity<TKey>
    {
        IMongoCollection<T> Collection { get; }
        Task Add(T entity);
        Task Add(IEnumerable<T> entities);
        Task Update(T entity);
        Task Update(IEnumerable<T> entities);
        Task Delete(TKey id);
        Task Delete(T entity);
        Task Delete(Expression<Func<T, bool>> predicate);
        Task<List<T>> FilterBy(Expression<Func<T, bool>> filterExpression);
        Task<T?> FindOne(Expression<Func<T, bool>> filterExpression);
    }

    public interface IRepository<T> : IRepository<T, string> where T : IEntity<string>
    {
    }
}
