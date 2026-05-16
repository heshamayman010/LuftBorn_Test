using System.Linq.Expressions;

namespace Luftborn.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        void AddRange(List<TEntity> entity);
        void Update(TEntity entity);

        bool IsExist(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllByThreadAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, dynamic>> order);
    Task<int> GetMaxByAsync<T, TKey>(Expression<Func<T, int>> selector,Expression<Func<T,bool>> filter) where T : class;

        Task<TEntity> GetFirstOrderByAsync(Expression<Func<TEntity, object>> expression);
        Task<TEntity> GetLastOrderByAsync(Expression<Func<TEntity, object>> expression);

        TEntity? GetById(int id);

        Task<int> CountAll(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> GetByIdAsync(int id);
        TEntity? GetBy(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);


        Task<IEnumerable<T>> Map_GetAllByAsync<T>(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<T>> Map_GetAllAsync<T>();


        Task<T> Map_GetByAsync<T>(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> expression, string including);
        Task<IQueryable<object>> GetAllSelectedByAsync(Expression<Func<TEntity, bool>> expression,
          Expression<Func<TEntity, object>> selector);
    }
}
