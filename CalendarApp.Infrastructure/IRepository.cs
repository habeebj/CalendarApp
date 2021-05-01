using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CalendarApp.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(string id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        // IEnumerable<TEntity> Find(ISpecification<TEntity> spec);
        // Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> spec);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        // int SaveChanges();
        // Task<int> SaveChangesAsync();
    }
}