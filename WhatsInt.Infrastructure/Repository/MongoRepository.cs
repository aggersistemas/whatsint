using System.Collections;
using System.Linq.Expressions;
using Infrastructure.Entities.Generic;
using Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repository
{
    public class MongoRepository<T, TKey> : IRepository<T, TKey> where T : IEntity<TKey>
    {
        public MongoRepository(IDatabaseSettings databaseSettings)
        {
            Collection = MongoUtil<TKey>.GetCollectionFromSettings<T>(databaseSettings);
        }

        public IMongoCollection<T> Collection { get; }

        public virtual async Task Add(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public virtual async Task Add(IEnumerable<T> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public virtual async Task Update(T entity)
        {
            await Collection.ReplaceOneAsync(model => model.Id != null && model.Id.Equals(entity.Id), entity);
        }

        public virtual async Task Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                await Collection.ReplaceOneAsync(model => model.Id != null && model.Id.Equals(entity.Id), entity);
            }
        }

        public virtual async Task Delete(TKey id)
        {
            await Collection.DeleteOneAsync(filter => filter.Id != null && filter.Id.Equals(id));
        }

        public virtual async Task Delete(T entity)
        {
            await Delete(entity.Id);
        }

        public virtual async Task Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in Collection.AsQueryable().Where(predicate))
            {
                await Delete(entity.Id);
            }
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Collection.AsQueryable().Any(predicate);
        }

        public virtual async Task<List<T>> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            var findAsync = await Collection.FindAsync(filterExpression);

            return findAsync.ToList();
        }

        public virtual async Task<T?> FindOne(Expression<Func<T, bool>> filterExpression)
        {
            var findAsync = await Collection.FindAsync(filterExpression);

            return findAsync.FirstOrDefault();
        }

        #region IQueryable<T>

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        public virtual Type ElementType => Collection.AsQueryable().ElementType;

        public virtual Expression Expression => Collection.AsQueryable().Expression;

        public virtual IQueryProvider Provider => Collection.AsQueryable().Provider;

        #endregion
    }

    public class MongoRepository<T> : MongoRepository<T, string>, IRepository<T> where T : IEntity<string>
    {
        public MongoRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }
    }
}
