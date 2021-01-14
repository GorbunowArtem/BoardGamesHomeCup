﻿// <auto-generated />
using System;
using HorCup.Presentation.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HorCup.Presentation.Migrations
{
    [DbContext(typeof(HorCupContext))]
    [Migration("20210112104720_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("HorCup.Presentation.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasScores")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("HorCup.Presentation.GamesStatistic.GameStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("AverageScore")
                        .HasColumnType("float");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastPlayedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("TimesPlayed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GamesStatistics");
                });

            modelBuilder.Entity("HorCup.Presentation.PlayScores.PlayScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("bit");

                    b.Property<Guid>("PlayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayScores");
                });

            modelBuilder.Entity("HorCup.Presentation.Players.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("HorCup.Presentation.PlayersStatistic.PlayerStatistic", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("AverageScore")
                        .HasColumnType("float");

                    b.Property<int>("PlayedTotal")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayersStatistics");
                });

            modelBuilder.Entity("HorCup.Presentation.Plays.Play", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PlayedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("HorCup.Presentation.GamesStatistic.GameStatistic", b =>
                {
                    b.HasOne("HorCup.Presentation.Games.Game", "Game")
                        .WithOne("GameStatistic")
                        .HasForeignKey("HorCup.Presentation.GamesStatistic.GameStatistic", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HorCup.Presentation.PlayScores.PlayScore", b =>
                {
                    b.HasOne("HorCup.Presentation.Plays.Play", null)
                        .WithMany("PlayerScores")
                        .HasForeignKey("PlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorCup.Presentation.Players.Player", "Player")
                        .WithMany("PlayScores")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("HorCup.Presentation.PlayersStatistic.PlayerStatistic", b =>
                {
                    b.HasOne("HorCup.Presentation.Games.Game", "Game")
                        .WithMany("PlayerStatistics")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorCup.Presentation.Players.Player", "Player")
                        .WithMany("PlayerStatistic")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("HorCup.Presentation.Plays.Play", b =>
                {
                    b.HasOne("HorCup.Presentation.Games.Game", "Game")
                        .WithMany("Plays")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HorCup.Presentation.Games.Game", b =>
                {
                    b.Navigation("GameStatistic");

                    b.Navigation("PlayerStatistics");

                    b.Navigation("Plays");
                });

            modelBuilder.Entity("HorCup.Presentation.Players.Player", b =>
                {
                    b.Navigation("PlayerStatistic");

                    b.Navigation("PlayScores");
                });

            modelBuilder.Entity("HorCup.Presentation.Plays.Play", b =>
                {
                    b.Navigation("PlayerScores");
                });
#pragma warning restore 612, 618
        }
    }
}
