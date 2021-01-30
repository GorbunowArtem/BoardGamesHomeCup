using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.GamesStatistic.Commands.GamePlayed;
using HorCup.Tests.GamesStatistic.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.GamesStatistic.Commands
{
	public class GamePlayedCommandHandlerTests : TestFixtureBase
	{
		private GamePlayedCommandHandler _sut;
		private readonly GamesStatisticsFactory _factory = new();

		[SetUp]
		public void SetUp()
		{
			_sut = new GamePlayedCommandHandler(Context, NullLogger<GamePlayedCommandHandler>.Instance);
		}

		[Test]
		public async Task Handle_GameStatisticNotExists_StatisticAdded()
		{
			await _sut.Handle(_factory.Commands.GamePlayedNotification, default);

			var stat = await Context.GamesStatistics.FirstAsync(g => g.GameId == _factory.CreatedGameStatisticId);

			stat.TimesPlayed.Should().Be(1);
			stat.LastPlayedDate.Should().Be(_factory.CreatedGameLastPlayedDate);
			stat.AverageScore.Should().Be(GamesStatisticsFactory.CreatedGameAvgScore);
		}

		[Test]
		public async Task Handle_GameStatisticExists_StatsUpdated()
		{
			await _sut.Handle(_factory.Commands.GamePlayedNotification, default);
			await _sut.Handle(
				_factory.Commands.GamePlayedNotification with
				{
					AverageScore = GamesStatisticsFactory.Game1AvgScore,
					LastPlayedDate = _factory.Game2LastPlayedDate
				}, default);

			var stat = await Context.GamesStatistics.FirstAsync(g => g.GameId == _factory.CreatedGameStatisticId);

			stat.TimesPlayed.Should().Be(2);
			stat.LastPlayedDate.Should().Be(_factory.Game2LastPlayedDate);
			stat.AverageScore.Should()
				.Be((GamesStatisticsFactory.CreatedGameAvgScore + GamesStatisticsFactory.Game1AvgScore) / 2);
		}

		[TearDown]
		public void TearDown()
		{
			Context.Database.EnsureDeleted();
		}
	}
}