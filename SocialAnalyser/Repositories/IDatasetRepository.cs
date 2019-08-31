using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public interface IDatasetRepository: IGenericRepository<Dataset>
  {
    Task<int> InsertDatasetAsync(string name, CancellationToken cancellationToken);
  }
}
