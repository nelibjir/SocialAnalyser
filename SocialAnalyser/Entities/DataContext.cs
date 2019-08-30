using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SocialAnalyser.Entities
{
  public class DataContext : DbContext
  {
    private readonly IDbConnectionConfiguration fConfiguration;
    private readonly ILoggerFactory fLoggerFactory;

    public DataContext(IDbConnectionConfiguration configuration, ILoggerFactory loggerFactory)
    {
      fConfiguration = configuration;
      fLoggerFactory = loggerFactory;
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseLoggerFactory(fLoggerFactory);
        optionsBuilder.UseSqlServer(fConfiguration.ConnectionString);
      }
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserFriend> UserFriend { get; set; }
  }
}
