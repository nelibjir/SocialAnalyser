using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Commands;
using SocialAnalyser.Services;

namespace SocialAnalyser.Handlers
{
  public class DatasetHandler: 
      IRequestHandler<CreateDatasetCommand>,
      IRequestHandler<GetDatasetStatisticsCommand, DatasetStatistics>
  {
    private readonly IDatasetService fDatasetServicecs;

    public DatasetHandler(
      IDatasetService datasetServicecs
      )
    {
      fDatasetServicecs = datasetServicecs;
    }

    public async Task<Unit> Handle(CreateDatasetCommand request, CancellationToken cancellationToken)
    {
      await fDatasetServicecs.CreateDatasetAsync(request.File, request.Name, cancellationToken);
      return Unit.Value;
    }

    public async Task<DatasetStatistics> Handle(GetDatasetStatisticsCommand request, CancellationToken cancellationToken)
    {
      return await fDatasetServicecs.GetDatasetStatisticsAsync(request.Name, cancellationToken);
    }
  }
}
