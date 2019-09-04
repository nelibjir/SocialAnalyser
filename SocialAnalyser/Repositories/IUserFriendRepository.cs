using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Dtos;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Repositories
{
  public interface IUserFriendRepository: IGenericRepository<UserFriend>
  {
    Task InsertAsync(UserFriendDto[] userFriendDtos, int datasetId, CancellationToken cancellationToken);
    Task<UserFriendsCountDto[]> FindByDatasetNameAsync(string datasetName, CancellationToken cancellationToken);
  }
}
