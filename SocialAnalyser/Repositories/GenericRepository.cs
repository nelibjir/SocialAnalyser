using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class GenericRepository<T>: IGenericRepository<T> where T : class, IBaseEntity
  {
    protected DataContext DbContext { get; }

    protected DbSet<T> DbSet => DbContext.Set<T>();

    public GenericRepository(DataContext dataContext)
    {
      DbContext = dataContext;
    }

    public void SaveAll()
    {
      DbContext.SaveChanges();
    }

    public async Task SaveAllAsync(CancellationToken cancellationToken)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T[]> FindManyAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken)
    {
      return await DbSet
        .Where(condition)
        .ToArrayAsync(cancellationToken);
    }

    public async Task<T[]> FindAllAsync(CancellationToken cancellationToken)
    {
      return await DbSet
        .ToArrayAsync();
    }
  }
}
