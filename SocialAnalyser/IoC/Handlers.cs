using Microsoft.Extensions.DependencyInjection;
using SocialAnalyser.Handlers.Exceptions;

namespace SocialAnalyser.IoC
{
  public static class Handlers
  {
    public static void AddHandlers(this IServiceCollection services)
    {
      services.AddTransient<IExceptionHandler, ApiExceptionHandler>();
    }
  }
}
