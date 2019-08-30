using Microsoft.Extensions.DependencyInjection;

namespace SocialAnalyser.IoC
{
  public static class Repositories
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddTransient<ICompanyRepository, CompanyRepository>();
    }
  }
}
