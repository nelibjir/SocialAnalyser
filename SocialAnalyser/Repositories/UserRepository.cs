using System.Collections.Generic;
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
  }
}
