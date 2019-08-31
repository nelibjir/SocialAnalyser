using Microsoft.Extensions.DependencyInjection;
using SocialAnalyser.Repositories;

namespace SocialAnalyser.IoC
{
  public static class Repositories
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddTransient<IDatasetRepository, DatasetRepository>();
      services.AddTransient<IUserFriendRepository, UserFriendRepository>();
      services.AddTransient<IUserRepository, UserRepository>();
    }
  }
}
