using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SocialAnalyser.Repositories
{
  public class DatasetRepository: IDatasetRepository
  {
    public Task<Unit> InsertDatasetAsync(string dataset, CancellationToken cancellationToken)
    {

    }
  }
}
