using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SocialAnalyser.Commands;
using SocialAnalyser.Services;

namespace SocialAnalyser.Handlers
{
  public class DatasetHandler
    : IRequestHandler<CreateDataSetCommand>
  {
    private readonly IDatasetService fDatasetServicecs;

    public DatasetHandler(
      IDatasetService datasetServicecs
      )
    {
      fDatasetServicecs = datasetServicecs;
    }

    public async Task<Unit> Handle(CreateDataSetCommand request, CancellationToken cancellationToken)
    {
      await fDatasetServicecs.CreateDatasetAsync(request.Dataset, request.Name, cancellationToken);
      return Unit.Value;
    }
  }
}
