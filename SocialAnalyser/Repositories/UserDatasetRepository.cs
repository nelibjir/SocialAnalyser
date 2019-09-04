using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class UserDatasetRepository: GenericRepository<UserDataset>, IUserDatasetRepository
  {
    public UserDatasetRepository(DataContext dataContext)
      : base(dataContext) { }

    public async Task InsertDatasetAsync(HashSet<string> userIds, int datasetId, CancellationToken cancellationToken)
    {
      foreach (string userId in userIds)
      {
        await DbSet.AddAsync(new UserDataset { UserId = userId , DatasetId = datasetId});
      }
    }
  }
}
