using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class GenericRepository<T>: IGenericRepository<T> where T : class, IBaseEntity
  {
    protected DataContext DataContext { get; }

    protected DbSet<T> DbSet => DataContext.Set<T>();

    public GenericRepository(DataContext dataContext)
    {
      DataContext = dataContext;
    }
  }
}
