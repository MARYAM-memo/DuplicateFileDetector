using System.Linq.Expressions;

namespace DFD.Core.Interfaces;

public interface IDashboardRepository<T> : IRepository<T> where T:class
{
      Task<TResult?> GetFileStatsAsync<TResult>(Expression<Func<IGrouping<int, T>, TResult>> selector);
}
