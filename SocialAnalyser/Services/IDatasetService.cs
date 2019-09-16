using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SocialAnalyser.Api.Models;

namespace SocialAnalyser.Services
{
  public interface IDatasetService
  {
    Task CreateDatasetAsync(IFormFile file, string name, CancellationToken cancellationToken);
    Task<DatasetStatistics> GetDatasetStatisticsAsync(string name, CancellationToken cancellationToken);
    Task<DatasetNames> GetDatasetNamesAsync(CancellationToken cancellationToken);
    Task<int> InsertDataSetNameAsync(string name, CancellationToken cancellationToken);
    Task<int> InsertNewUsersAsync(ISet<string> userIds, CancellationToken cancellationToken);
  }
}
