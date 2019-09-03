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
  }
}
