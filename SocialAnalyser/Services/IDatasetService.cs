using System.Threading;
using System.Threading.Tasks;

namespace SocialAnalyser.Services
{
  public interface IDatasetService
  {
    Task CreateDatasetAsync(string dataset, string name, CancellationToken cancellationToken);
  }
}
