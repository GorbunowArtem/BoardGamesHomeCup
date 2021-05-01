﻿// <auto-generated />
using System;
using HorCup.Plays.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HorCup.Plays.Migrations
{
    [DbContext(typeof(PlaysContext))]
    [Migration("20210501061048_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.ToTable("PlayScores");
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

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("HorCup.Presentation.PlayScores.PlayScore", b =>
                {
                    b.HasOne("HorCup.Presentation.Plays.Play", null)
                        .WithMany("PlayerScores")
                        .HasForeignKey("PlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HorCup.Presentation.Plays.Play", b =>
                {
                    b.Navigation("PlayerScores");
                });
#pragma warning restore 612, 618
        }
    }
}