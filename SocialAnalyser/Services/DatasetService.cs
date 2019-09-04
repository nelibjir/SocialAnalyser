using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Dtos;
using SocialAnalyser.Repositories;
using SocialAnalyser.Utils;

namespace SocialAnalyser.Services
{
  public class DatasetService: IDatasetService
  {
    private readonly IDatasetRepository fDatasetRepository;
    private readonly IUserDatasetRepository fUserDatasetRepository;
    private readonly IUserFriendRepository fUserFriendRepository;
    private readonly IUserRepository fUserRepository;

    public DatasetService(
      IDatasetRepository datasetRepository,
      IUserFriendRepository userFriendRepository,
      IUserRepository userRepository,
      IUserDatasetRepository userDatasetRepository
      )
    {
      fDatasetRepository = datasetRepository;
      fUserDatasetRepository = userDatasetRepository;
      fUserFriendRepository = userFriendRepository;
      fUserRepository = userRepository;
    }

    public async Task CreateDatasetAsync(IFormFile file, string name, CancellationToken cancellationToken)
    {

      UserFriendDto[] relationships = ParseDataset(await FormFileExtensionUtil.ReadAsListAsync(file))
        .ToArray();

      HashSet<string> userIds = getAllUsers(relationships);

      int datasetId = await fDatasetRepository.InsertDatasetAsync(name, cancellationToken);

      await fUserRepository.InsertUsersAsync(userIds, cancellationToken);
      await fUserRepository.SaveAllAsync(cancellationToken);

      await fUserDatasetRepository.InsertDatasetAsync(userIds, datasetId ,cancellationToken);
      await fUserDatasetRepository.SaveAllAsync(cancellationToken);

      await fUserFriendRepository.InsertAsync(relationships, datasetId, cancellationToken);
      await fUserFriendRepository.SaveAllAsync(cancellationToken);
    }

    public async Task<DatasetStatistics> GetDatasetStatisticsAsync(string name, CancellationToken cancellationToken)
    {
      UserFriendsCountDto[] userFriends = await fUserFriendRepository.FindByDatasetNameAsync(name, cancellationToken);

      if (userFriends.Length == 0)
        return null;
        // throw exception or code

      return new DatasetStatistics
      {
        AvgNumberOfFreinds = (int)Math.Round(userFriends.Average(a => a.FriendsCount)),
        NumberOfUsers = userFriends.Length
      };

    }

    private IEnumerable<UserFriendDto> ParseDataset(string dataset)
    {
      return dataset
        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(rel => rel.Split(null))
        .Select(tuple => new UserFriendDto { UserId = tuple[0], FriendId = tuple[1] });
    }

    private HashSet<string> getAllUsers(UserFriendDto[] relationships)
    {
      HashSet<string> userIds = new HashSet<string>();
      foreach (UserFriendDto userFriendDto in relationships)
      {
        userIds.Add(userFriendDto.UserId);
        userIds.Add(userFriendDto.FriendId);
      }

      return userIds;
    }
  }
}
