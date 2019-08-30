using Microsoft.Extensions.DependencyInjection;
using SocialAnalyser.Factories;

namespace SocialAnalyser.IoC
{
  public static class Factories
  {
    public static void AddFactories(this IServiceCollection services)
    {
      services.AddTransient<ApiErrorFactory>();
      services.AddTransient<ExceptionHandlerFactory>();
    }
  }
}
