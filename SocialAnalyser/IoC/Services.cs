using Microsoft.Extensions.DependencyInjection;
using SocialAnalyser.Services;

namespace SocialAnalyser.IoC
{
  public static class Services
  {
    public static void AddServices(this IServiceCollection services)
    {
      services.AddTransient<IDatasetService, DatasetService>();
    }
  }
}
