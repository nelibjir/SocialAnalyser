using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public interface IUserDatasetRepository: IGenericRepository<UserDataset>
  {
    Task InsertDatasetAsync(HashSet<string> userIds, int datasetId, CancellationToken cancellationToken);
  }
}
