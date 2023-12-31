﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GamesWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230703154924_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entity.AgeRating", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("AgeRatingId");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("AgeRatingId");

                    b.ToTable("AgeRatings", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("GameId");

                    b.Property<string>("AgeRatingId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(10,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("GameId");

                    b.HasIndex("AgeRatingId");

                    b.ToTable("Games", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.GameGenre", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GenresId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("GamesId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("GameId", "GenresId");

                    b.HasIndex("GamesId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameGenres");
                });

            modelBuilder.Entity("Domain.Entity.GamePlatform", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlatformsId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("GamesId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("GameId", "PlatformsId");

                    b.HasIndex("GamesId");

                    b.HasIndex("PlatformsId");

                    b.ToTable("GamePlatforms");
                });

            modelBuilder.Entity("Domain.Entity.Genre", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Platform", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("PlatformId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PlatformId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Platforms", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Game", b =>
                {
                    b.HasOne("Domain.Entity.AgeRating", "AgeRating")
                        .WithMany("Games")
                        .HasForeignKey("AgeRatingId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AgeRating");
                });

            modelBuilder.Entity("Domain.Entity.GameGenre", b =>
                {
                    b.HasOne("Domain.Entity.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Domain.Entity.GamePlatform", b =>
                {
                    b.HasOne("Domain.Entity.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("Domain.Entity.AgeRating", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
