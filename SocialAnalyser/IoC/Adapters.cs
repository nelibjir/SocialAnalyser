using Microsoft.Extensions.DependencyInjection;
using SocialAnalyser.Adapters;

namespace SocialAnalyser.IoC
{
  public static class Adapters
  {
    public static void AddAdapters(this IServiceCollection services)
    {
      services.AddTransient<IStringUtilAdapter, StringUtilAdapter>();
    }
  }
}
