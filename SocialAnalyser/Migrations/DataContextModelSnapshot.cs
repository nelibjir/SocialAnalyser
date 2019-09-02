﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialAnalyser.Entities;

namespace SocialAnalyser.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialAnalyser.Entities.Dataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_dataset-name");

                    b.ToTable("datasets");
                });

            modelBuilder.Entity("SocialAnalyser.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasName("IX_user-user_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("SocialAnalyser.Entities.UserFriend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DatasetId")
                        .HasColumnName("dataset_id");

                    b.Property<string>("FriendUserId")
                        .IsRequired()
                        .HasColumnName("friend_user_id");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId")
                        .HasName("IX_userfriend-dataset_id");

                    b.HasIndex("FriendUserId")
                        .HasName("IX_userfriend-frienduser_id");

                    b.HasIndex("UserId")
                        .HasName("IX_userfriend-user_id");

                    b.ToTable("users_friends");
                });

            modelBuilder.Entity("SocialAnalyser.Entities.UserFriend", b =>
                {
                    b.HasOne("SocialAnalyser.Entities.Dataset", "Dataset")
                        .WithMany("UserFriends")
                        .HasForeignKey("DatasetId")
                        .HasConstraintName("FK_userfriend-dataset_id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SocialAnalyser.Entities.User", "FriendUser")
                        .WithMany("UserFriends")
                        .HasForeignKey("FriendUserId")
                        .HasConstraintName("FK_userfriend-frienduser_id")
                        .HasPrincipalKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SocialAnalyser.Entities.User", "User")
                        .WithMany("Users")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_userfriend-user_id")
                        .HasPrincipalKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
