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
    private readonly IDatasetServicecs fDatasetServicecs;

    public DatasetHandler(
      IDatasetServicecs datasetServicecs
      )
    {
      fDatasetServicecs = datasetServicecs;
    }

    public async Task<Unit> Handle(CreateDataSetCommand request, CancellationToken cancellationToken)
    {

    }
  }
}
