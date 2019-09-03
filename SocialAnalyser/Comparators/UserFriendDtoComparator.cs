using System;
using System.Collections.Generic;
using SocialAnalyser.Dtos;

namespace SocialAnalyser.Comparators
{
  public class UserFriendDtoComparator: IEqualityComparer<UserFriendDto>
  {
    public bool Equals(UserFriendDto x, UserFriendDto y)
    {
      return x.UserId.Equals(y.UserId, StringComparison.InvariantCultureIgnoreCase);
    }

    public int GetHashCode(UserFriendDto obj)
    {
      return obj.UserId.GetHashCode();
    }
  }
}
