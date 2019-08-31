using System.Threading;
using System.Threading.Tasks;
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
  }
}
