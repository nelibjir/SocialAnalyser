using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SocialAnalyser.Repositories
{
  public interface IDatasetRepository
  {
    Task<Unit> InsertDatasetAsync(string dataset, CancellationToken cancellationToken);
  }
}
