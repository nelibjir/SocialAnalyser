using MediatR;
using SocialAnalyser.Api.Models;

namespace SocialAnalyser.Commands
{
  public class GetDatasetStatisticsCommand: IRequest<DatasetStatistics>
  {
    public string Name { get; set; }
  }
}
