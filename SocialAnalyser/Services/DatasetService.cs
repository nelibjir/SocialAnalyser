using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SocialAnalyser.Comparators;
using SocialAnalyser.Dtos;
using SocialAnalyser.Repositories;
using SocialAnalyser.Utils;

namespace SocialAnalyser.Services
{
  public class DatasetService: IDatasetService
  {
    private readonly IDatasetRepository fDatasetRepository;
    private readonly IUserFriendRepository fUserFriendRepository;
    private readonly IUserRepository fUserRepository;


    public DatasetService(
      IDatasetRepository datasetRepository,
      IUserFriendRepository userFriendRepository,
      IUserRepository userRepository
      )
    {
      fDatasetRepository = datasetRepository;
      fUserFriendRepository = userFriendRepository;
      fUserRepository = userRepository;
    }

    public async Task CreateDatasetAsync(IFormFile file, string name, CancellationToken cancellationToken)
    {

      UserFriendDto[] relationships = ParseDataset(await FormFileExtensionUtil.ReadAsListAsync(file))
        .ToArray();

      HashSet<string> userIds = new HashSet<string>();
      foreach(UserFriendDto userFriendDto in relationships)
      {
        userIds.Add(userFriendDto.UserId);
        userIds.Add(userFriendDto.FriendId);
      }

      int datasetId = await fDatasetRepository.InsertDatasetAsync(name, cancellationToken);
      await fUserRepository.InsertUsersAsync(userIds, cancellationToken);
      await fUserFriendRepository.SaveAllAsync(cancellationToken);

      await fUserFriendRepository.InsertAsync(relationships, datasetId, cancellationToken);
      await fUserFriendRepository.SaveAllAsync(cancellationToken);
    }

    private IEnumerable<UserFriendDto> ParseDataset(string dataset)
    {
      return dataset
        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(rel => rel.Split(null))
        .Select(tuple => new UserFriendDto { UserId = tuple[0], FriendId = tuple[1] });
    }
  }
}
