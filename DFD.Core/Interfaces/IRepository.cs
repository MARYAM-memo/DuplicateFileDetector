using System.Linq.Expressions;

namespace DFD.Core.Interfaces;

public interface IRepository<T> where T : class
{
      Task<T?> FindAsync(int id);
      Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] args);
      void Add(T entity);
      void Delete(T entity);
      Task ExecuteDeleteAsync();
      Task<int> CountAsync();
      void Update(T entity);
      Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false);
      Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false, params Expression<Func<T, object>>[] args);
      Task<IEnumerable<TResult>> FetchAsync<TResult>(Expression<Func<T, TResult>> selector, bool withNoTracking = false, params Expression<Func<T, object>>[] args);
      Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false);
      Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false, params Expression<Func<T, object>>[] args);
}
