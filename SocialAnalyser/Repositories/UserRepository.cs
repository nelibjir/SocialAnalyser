using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Dtos;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class UserRepository: GenericRepository<User>, IUserRepository
  {
    public UserRepository(DataContext dataContext)
      : base(dataContext) { }

    public async Task InsertUsersAsync(HashSet<string> userIds, CancellationToken cancellationToken)
    {
      foreach (string userId in userIds)
      {
        await DbSet.AddAsync(new User { UserId = userId });
      }
    }

    public async Task<int> FindCountByDatasetNameAsync(string datasetName, CancellationToken cancellationToken)
    {
      return await DbSet
       .Join(DbContext.UserDataset,
          user => user.UserId,
          userDataset => userDataset.UserId,
          (user, userDataset) => new { user, userDataset })
       .Join(DbContext.Dataset,
         userUserDataset => userUserDataset.userDataset.DatasetId,
         dataset => dataset.Id,
         (userUserDataset, dataset) => new { userUserDataset, dataset }
       )
       .Where(res => res.dataset.Name == datasetName)
       .CountAsync();
    }
  }
}
