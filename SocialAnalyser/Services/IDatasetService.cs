using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialAnalyser.Services
{
  public interface IDatasetService
  {
    Task CreateDatasetAsync(IFormFile file, string name, CancellationToken cancellationToken);
  }
}
