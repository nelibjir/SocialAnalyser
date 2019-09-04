using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public interface IUserRepository: IGenericRepository<User>
  {
    Task InsertUsersAsync(HashSet<string> userIds, CancellationToken cancellationToken);
    Task<int> FindCountByDatasetNameAsync(string datasetName, CancellationToken cancellationToken);
  }
}
