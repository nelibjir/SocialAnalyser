using System;
using System.Collections.Generic;
using System.Linq;
using SocialAnalyser.Dtos;
using SocialAnalyser.Exceptions;

namespace SocialAnalyser.Utils
{
  public class StringUtil
  {
    private const int _MaxUserIdLength = 450;

    /// <summary>
    /// Process the string and return it in the form of UserFriendDto (UserId -> FriendId)
    /// </summary>
    /// <param name="dataset">The dataset that should be transformed</param>
    /// <returns>Collection of dataset in the form of UserFriendDto</returns>
    /// <exception cref="400">The parameter dataset in in the wrong format, for more see the exception</exception>
    public static IEnumerable<UserFriendDto> ParseDataset(string dataset)
    {
      if (string.IsNullOrEmpty(dataset))
        return null;

      return dataset
        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(rel => rel.Split(null))
        .Select(tuple =>
        { 
          if (tuple.Length != 2)
            throw new BadRequestException("Content of the file is not in the correct format. Example : \"1 2\n\"");
          if (tuple[0].Length > _MaxUserIdLength)
            throw new BadRequestException($"id : {tuple[0]} is too long");
          if (tuple[1].Length > _MaxUserIdLength)
            throw new BadRequestException($"id : {tuple[1]} is too long");

          return new UserFriendDto { UserId = tuple[0], FriendId = tuple[1] };
        });
    }
  }
}
