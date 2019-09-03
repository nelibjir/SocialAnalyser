using System.Threading;
using System.Threading.Tasks;

namespace SocialAnalyser.Repositories
{
  public interface IGenericRepository<T>
  {
    void SaveAll();
    Task SaveAllAsync(CancellationToken cancellationToken);
  }
}
