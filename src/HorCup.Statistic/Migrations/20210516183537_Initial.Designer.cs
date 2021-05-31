﻿// <auto-generated />
using System;
using HorCup.Statistic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HorCup.Statistic.Migrations
{
    [DbContext(typeof(StatisticContext))]
    [Migration("20210516183537_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HorCup.Statistic.GamesStatistic.GameStatistic", b =>
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

                    b.ToTable("GamesStatistics");
                });

            modelBuilder.Entity("HorCup.Statistic.PlayersStatistic.PlayerStatistic", b =>
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

                    b.ToTable("PlayersStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
