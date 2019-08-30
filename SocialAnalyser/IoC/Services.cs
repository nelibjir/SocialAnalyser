using Microsoft.Extensions.DependencyInjection;

namespace SocialAnalyser.IoC
{
  public static class Services
  {
    public static void AddServices(this IServiceCollection services)
    {
      services.AddTransient<ICompanyService, CompanyService>();
    }
  }
}
