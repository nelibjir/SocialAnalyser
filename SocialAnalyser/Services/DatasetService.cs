using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SocialAnalyser.Dtos;
using SocialAnalyser.Repositories;

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

    public async Task CreateDatasetAsync(string dataset, string name, CancellationToken cancellationToken)
    {
      UserFriendDto[] relationships = ParseDataset(dataset).ToArray();
      int datasetId = await fDatasetRepository.InsertDatasetAsync(name, cancellationToken);
      await fUserRepository.InsertUsersAsync(relationships, cancellationToken);
      await fUserFriendRepository.InsertAsync(relationships, datasetId, cancellationToken);
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
