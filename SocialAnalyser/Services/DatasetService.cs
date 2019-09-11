using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Dtos;
using SocialAnalyser.Exceptions;
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

    private const int _MaxUserIdLength = 450;

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

      int datasetId = InsertDataSetNameAsync(name, cancellationToken).Result;

      await InsertNewUsersAsync(userIds, cancellationToken);

      await fUserDatasetRepository.InsertDatasetAsync(userIds, datasetId, cancellationToken);
      await fUserDatasetRepository.SaveAllAsync(cancellationToken);

      await fUserFriendRepository.InsertAsync(relationships, datasetId, cancellationToken);
      await fUserFriendRepository.SaveAllAsync(cancellationToken);
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

    public async Task InsertNewUsersAsync(ISet<string> userIds, CancellationToken cancellationToken)
    {
      string[] userIdsInDb = (await fUserRepository.FindManyAsync(users => userIds.Contains(users.UserId), cancellationToken))
       .Select(u => u.UserId)
       .ToArray();

      HashSet<string> newUserIds = userIds
        .Where(u => !userIdsInDb.Contains(u))
        .ToHashSet();

      await fUserRepository.InsertUsersAsync(newUserIds, cancellationToken);
      await fUserRepository.SaveAllAsync(cancellationToken);
    }

    private IEnumerable<UserFriendDto> ParseDataset(string dataset)
    {
      return dataset
        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(rel => rel.Split(null))
        .Select(tuple =>
        {

          if (tuple.Length != 2)
            throw new BadRequestException("Content of the file is not in the correct format");
          if (tuple[0].Length > _MaxUserIdLength)
            throw new BadRequestException($"id : {tuple[0]} is too long");
          if (tuple[1].Length > _MaxUserIdLength)
            throw new BadRequestException($"id : {tuple[1]} is too long");

          return new UserFriendDto { UserId = tuple[0], FriendId = tuple[1] };
        }
        );
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

    public async Task<DatasetNames> GetDatasetNamesAsync(CancellationToken cancellationToken)
    {
      return new DatasetNames
      {
        Names = (await fDatasetRepository.FindAllAsync(cancellationToken))
        .Select(ds => ds.Name)
        .ToArray()
      };
    }
  }
}
