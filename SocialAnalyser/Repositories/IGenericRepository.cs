using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialAnalyser.Repositories
{
  public interface IGenericRepository<T>
  {
    void SaveAll();
    Task SaveAllAsync(CancellationToken cancellationToken);
    Task<T[]> FindManyAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken);
  }
}
