using System.Linq.Expressions;
using DFD.Core.Interfaces;
using DFD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DFD.Infrastructure.DataAccess;

public class Repository<T>(DatabaseContext ctx) : IRepository<T> where T : class
{
      readonly DatabaseContext context = ctx;
      DbSet<T> Set => context.Set<T>();

      public async Task<T?> FindAsync(int id)
      {
            return await Set.FindAsync(id);
      }

      public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = Set;
            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }
            return await query.FirstOrDefaultAsync(predicate);
      }

      public void Add(T entity)
      {
            Set.Add(entity);
      }

      public void Delete(T entity)
      {
            Set.Remove(entity);
      }

      public async Task ExecuteDeleteAsync()
      {
            await Set.ExecuteDeleteAsync();
      }
      public async Task<int> CountAsync()
      {
            return await Set.CountAsync();
      }

      public void Update(T entity)
      {
            Set.Update(entity);
      }

      public async Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false)
      {
            if (withNoTracking) return await Set.AsNoTracking().ToListAsync();
            else return await Set.ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = Set;
            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }
            if (withNoTracking) return await query.AsNoTracking().ToListAsync();
            else return await query.ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false)
      {
            IQueryable<T> query = Set;
            if (withNoTracking) return await query.Where(predicate).AsNoTracking().ToListAsync();
            else return await query.Where(predicate).ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = Set;
            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }

            if (withNoTracking) return await query.Where(predicate).AsNoTracking().ToListAsync();
            else return await query.Where(predicate).ToListAsync();
      }

      public async Task<IEnumerable<TResult>> FetchAsync<TResult>(Expression<Func<T, TResult>> selector, bool withNoTracking = false, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = Set;
            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }
            if (withNoTracking) return await query.AsNoTracking().Select(selector).ToListAsync();
            else return await query.Select(selector).ToListAsync();
      }

}
