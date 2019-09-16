using System.Collections.Generic;
using SocialAnalyser.Dtos;

namespace SocialAnalyser.Adapters
{
  public interface IStringUtilAdapter
  {
    IEnumerable<UserFriendDto> GetUserFriendDtos(string dataset);
  }
}
