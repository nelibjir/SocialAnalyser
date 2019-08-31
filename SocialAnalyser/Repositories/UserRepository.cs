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

    public async Task InsertUsersAsync(UserFriendDto[] userFriendDtos, CancellationToken cancellationToken)
    {
      foreach (UserFriendDto userFriend in userFriendDtos)
      {
        await DbSet.AddAsync(new User { UserId = userFriend.UserId });
      }
    }
  }
}
