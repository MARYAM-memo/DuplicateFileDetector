using System.Linq.Expressions;
using DFD.Core.Interfaces;
using DFD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DFD.Infrastructure.DataAccess;

public class DashboardRepository<T>(DatabaseContext ctx) : Repository<T>(ctx), IDashboardRepository<T> where T : class
{
      readonly DatabaseContext context = ctx;
      public async Task<TResult?> GetFileStatsAsync<TResult>(Expression<Func<IGrouping<int, T>, TResult>> selector)
      {
            return await context.Set<T>().AsNoTracking().GroupBy(x => 1).Select(selector).FirstOrDefaultAsync();
      }
}
