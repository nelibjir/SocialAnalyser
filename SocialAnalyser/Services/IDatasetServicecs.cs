
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SocialAnalyser.Services
{
  public interface IDatasetServicecs
  {
    Task<Unit> CreateDatasetAsync(string dataset, CancellationToken cancellationToken);
  }
}
