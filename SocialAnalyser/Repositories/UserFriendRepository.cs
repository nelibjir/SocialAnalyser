using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Dtos;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public class UserFriendRepository: GenericRepository<UserFriend>, IUserFriendRepository
  {
    public UserFriendRepository(DataContext dataContext)
      : base(dataContext) { }

    public async Task InsertAsync(UserFriendDto[] userFriendDtos, int datasetId, CancellationToken cancellationToken)
    {
      foreach (UserFriendDto userFriend in userFriendDtos)
      {
        await DbSet.AddAsync(new UserFriend { UserId = userFriend.UserId, FriendUserId = userFriend.FriendId, DatasetId = datasetId });
      }
    }

    public async Task<UserFriendsCountDto[]> FindByDatasetNameAsync(string datasetName, CancellationToken cancellationToken)
    {
      return await DbSet
        .Where(userFriend => userFriend.Dataset.Name == datasetName)
        .GroupBy(
          user => user.UserId,
          friends => friends.FriendUserId,
          (user, friends) => new UserFriendsCountDto { UserId = user, FriendsCount = friends.ToArray().Length }
        )
        .ToArrayAsync(cancellationToken);
    }
  }
}
