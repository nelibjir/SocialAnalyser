using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Dtos;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public interface IUserRepository: IGenericRepository<User>
  {
    Task InsertUsersAsync(HashSet<string> userIds, CancellationToken cancellationToken); 
  }
}
