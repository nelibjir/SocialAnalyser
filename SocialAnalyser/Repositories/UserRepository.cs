using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

    public async Task<int> FindUsersByDatasetName(string datasetName, CancellationToken cancellationToken)
    {
      /**return DbSet
       .Join(DbContext.UserFriend,
          user => user.UserId,
          userFriend => userFriend.UserId,
          (user, userFriend) => new { user, userFriend })
       .Join(DbContext.Dataset,
         join => join.userFriend.DatasetId,
         dataset => dataset.Id,
         (join, dataset) => 
       )     **/
      return 0;
    }
  }
}
