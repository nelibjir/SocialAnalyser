using System.Collections.Generic;
using SocialAnalyser.Dtos;
using SocialAnalyser.Utils;

namespace SocialAnalyser.Adapters
{
  public class StringUtilAdapter: IStringUtilAdapter
  {
    public IEnumerable<UserFriendDto> GetUserFriendDtos(string dataset)
    {
      return StringUtil.ParseDataset(dataset);
    }
  }
}
