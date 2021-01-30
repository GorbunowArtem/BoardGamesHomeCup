using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorCup.Presentation.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    MinPlayers = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasScores = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamesStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimesPlayed = table.Column<int>(type: "int", nullable: false),
                    AverageScore = table.Column<double>(type: "float", nullable: true),
                    LastPlayedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesStatistics_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plays_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersStatistics",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayedTotal = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    AverageScore = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersStatistics", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayersStatistics_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayersStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayScores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayScores_Plays_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Plays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamesStatistics_GameId",
                table: "GamesStatistics",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersStatistics_PlayerId",
                table: "PlayersStatistics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_GameId",
                table: "Plays",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayScores_PlayerId",
                table: "PlayScores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayScores_PlayId",
                table: "PlayScores",
                column: "PlayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesStatistics");

            migrationBuilder.DropTable(
                name: "PlayersStatistics");

            migrationBuilder.DropTable(
                name: "PlayScores");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Plays");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
