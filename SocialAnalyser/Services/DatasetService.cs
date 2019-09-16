using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Adapters;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Dtos;
using SocialAnalyser.Exceptions;
using SocialAnalyser.Extensions;
using SocialAnalyser.Repositories;

namespace SocialAnalyser.Services
{
  public class DatasetService: IDatasetService
  {
    private readonly IDatasetRepository fDatasetRepository;
    private readonly IUserDatasetRepository fUserDatasetRepository;
    private readonly IUserFriendRepository fUserFriendRepository;
    private readonly IUserRepository fUserRepository;

    private readonly IStringUtilAdapter fStringUtilAdapter;

    public DatasetService(
      IDatasetRepository datasetRepository,
      IUserFriendRepository userFriendRepository,
      IUserRepository userRepository,
      IUserDatasetRepository userDatasetRepository,
      IStringUtilAdapter stringUtilAdapter
      )
    {
      fDatasetRepository = datasetRepository;
      fUserDatasetRepository = userDatasetRepository;
      fUserFriendRepository = userFriendRepository;
      fUserRepository = userRepository;
      fStringUtilAdapter = stringUtilAdapter;
    }

    public async Task CreateDatasetAsync(IFormFile file, string name, CancellationToken cancellationToken)
    {

      UserFriendDto[] relationships = fStringUtilAdapter.GetUserFriendDtos(await FormFileExtension.ReadAsListAsync(file))
        .ToArray();
      HashSet<string> userIds = getAllUsers(relationships);

      int datasetId = InsertDataSetNameAsync(name, cancellationToken).Result;

      await InsertNewUsersAsync(userIds, cancellationToken);

      await fUserDatasetRepository.InsertDatasetAsync(userIds, datasetId, cancellationToken);
      await fUserDatasetRepository.SaveAllAsync(cancellationToken);

      await fUserFriendRepository.InsertAsync(relationships, datasetId, cancellationToken);
      await fUserFriendRepository.SaveAllAsync(cancellationToken);
    }

    public async Task<DatasetNames> GetDatasetNamesAsync(CancellationToken cancellationToken)
    {
      return new DatasetNames
      {
        Names = (await fDatasetRepository.FindAllAsync(cancellationToken))
        .Select(ds => ds.Name)
        .ToArray()
      };
    }

    public async Task<DatasetStatistics> GetDatasetStatisticsAsync(string name, CancellationToken cancellationToken)
    {
      UserFriendsCountDto[] userFriends = await fUserFriendRepository.FindByDatasetNameAsync(name, cancellationToken);

      if (userFriends.Length == 0)
        throw new NotFoundException(name);

      return new DatasetStatistics
      {
        AvgNumberOfFreinds = (int)Math.Round(userFriends.Average(a => a.FriendsCount)),
        NumberOfUsers = await fUserRepository.FindCountByDatasetNameAsync(name, cancellationToken)
      };
    }

    /// <summary>
    ///  Insert dataset name
    /// </summary>
    /// <param name="name">Name of the dataset</param>
    /// <param name="cancellationToken">Cancelation token</param>
    /// <returns>Primary id of the inserted dataset</returns>
    /// <exception cref="ConflictException">Dataset with the given name is already in the table</exception>
    public async Task<int> InsertDataSetNameAsync(string name, CancellationToken cancellationToken)
    {
      try
      {
        return await fDatasetRepository.InsertDatasetAsync(name, cancellationToken);
      }
      catch (DbUpdateException)
      {
        throw new ConflictException(name);
      }
    }

    public async Task<int> InsertNewUsersAsync(ISet<string> userIds, CancellationToken cancellationToken)
    {
      if (userIds == null || userIds.Count == 0)
        return 0;

      string[] userIdsInDb = (await fUserRepository.FindManyAsync(users => userIds.Contains(users.UserId), cancellationToken))
       .Select(u => u.UserId)
       .ToArray();

      HashSet<string> newUserIds = userIds
        .Where(u => !userIdsInDb.Contains(u))
        .ToHashSet();

      await fUserRepository.InsertUsersAsync(newUserIds, cancellationToken);
      return await fUserRepository.SaveAllAsync(cancellationToken);
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
