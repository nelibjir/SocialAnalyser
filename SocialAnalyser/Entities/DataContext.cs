using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SocialAnalyser.Entities
{
  public class DataContext: DbContext
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
    public virtual DbSet<Dataset> Dataset { get; set; }
    public virtual DbSet<UserDataset> UserDataset { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

      modelBuilder.Entity<User>(entity =>
      {
        entity.HasIndex(e => e.UserId)
                  .HasName("IX_user-user_id")
                  .IsUnique();
      });

      modelBuilder.Entity<UserFriend>(entity =>
      {
        entity.HasIndex(e => e.UserId)
                  .HasName("IX_userfriend-user_id");
        entity.HasIndex(e => e.FriendUserId)
                  .HasName("IX_userfriend-frienduser_id");
        entity.HasIndex(e => e.DatasetId)
                  .HasName("IX_userfriend-dataset_id");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Users)
                  .HasForeignKey(d => d.UserId)
                  .HasPrincipalKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_userfriend-user_id");

        entity.HasOne(d => d.FriendUser)
                  .WithMany(p => p.UserFriends)
                  .HasForeignKey(d => d.FriendUserId)
                  .HasPrincipalKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_userfriend-frienduser_id");

        entity.HasOne(d => d.Dataset)
                  .WithMany(p => p.UserFriends)
                  .HasForeignKey(d => d.DatasetId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_userfriend-dataset_id");
      });

      modelBuilder.Entity<Dataset>(entity =>
      {
        entity.HasIndex(e => e.Name)
                  .HasName("IX_dataset-name")
                  .IsUnique();
      });

      modelBuilder.Entity<UserDataset>(entity =>
      {
        entity.HasIndex(e => e.UserId)
                  .HasName("IX_userdataset-user_id");
        entity.HasIndex(e => e.DatasetId)
                  .HasName("IX_userdataset-dataset_id");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserDatasets)
                  .HasForeignKey(d => d.UserId)
                  .HasPrincipalKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_userdatasets-user_id");

        entity.HasOne(d => d.Dataset)
                  .WithMany(p => p.UserDatasets)
                  .HasForeignKey(d => d.DatasetId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_userdatasets-dataset_id");
      });
    }
  }
}
