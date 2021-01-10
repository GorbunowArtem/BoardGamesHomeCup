using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.PlayersStatistic;
using HorCup.Presentation.PlayersStatistic.Commands;
using HorCup.Tests.GamesStatistic.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.PlayersStatistic.Commands
{
	public class PlayerPlayedCommandHandlerTests : TestFixtureBase
	{
		private readonly GamesStatisticsFactory _factory = new();
		private PlayerPlayedCommandHandler _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new PlayerPlayedCommandHandler(Context, NullLogger<PlayerPlayedCommandHandler>.Instance);
		}

		[Test]
		public async Task Handle_StatForPlayerNotExists_StatAdded()
		{
			await _sut.Handle(_factory.Commands.GamePlayedNotification, default);

			var playerStats = await Context.PlayersStatistics.ToArrayAsync();

			playerStats.Should()
				.BeEquivalentTo(new PlayerStatistic[]
				{
					new()
					{
						GameId = _factory.CreatedGameStatisticId,
						PlayerId = _factory.PlayersFactory.Player1Id,
						Wins = 1,
						AverageScore = GamesStatisticsFactory.Player1Game1Score
					},
					new()
					{
						GameId = _factory.CreatedGameStatisticId,
						PlayerId = _factory.PlayersFactory.Player2Id,
						Wins = 0,
						AverageScore = GamesStatisticsFactory.Player2Game1Score
					}
				});
		}

		[Test]
		public async Task Handle_StatForPlayerExists_StatUpdated()
		{
			await _sut.Handle(_factory.Commands.GamePlayedNotification, default);
			await _sut.Handle(
				_factory.Commands.GamePlayedNotification with {PlayerScore = _factory.UpdatedGamePlayerScores1},
				default);

			var playerStats = await Context.PlayersStatistics.ToArrayAsync();

			playerStats.Should()
				.BeEquivalentTo(new PlayerStatistic[]
				{
					new()
					{
						GameId = _factory.CreatedGameStatisticId,
						PlayerId = _factory.PlayersFactory.Player1Id,
						Wins = 2,
						AverageScore = (GamesStatisticsFactory.Player1Game1Score +
						                GamesStatisticsFactory.Player1Game2Score) / 2
					},
					new()
					{
						GameId = _factory.CreatedGameStatisticId,
						PlayerId = _factory.PlayersFactory.Player2Id,
						Wins = 0,
						AverageScore = (GamesStatisticsFactory.Player2Game1Score +
						                GamesStatisticsFactory.Player2Game2Score) / 2
					}
				});
		}

		[TearDown]
		public void TearDown()
		{
			Context.Database.EnsureDeleted();
		}
	}
}