using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.PlayersStatistic.Commands;
using HorCup.Tests.GamesStatistic.Factory;
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

		// [Test]
		// public async Task Handle_StatForPlayerExists_StatUpdated()
		// {
		// 	await _sut.Handle(_factory.Commands.GamePlayedNotification, default);
		// 	await _sut.Handle(
		// 		_factory.Commands.GamePlayedNotification with {PlayerScore = _factory.UpdatedGamePlayerScores1},
		// 		default);
		//
		// 	var player1Stats = Context.PlayersStatistics.First(p =>
		// 		p.GameId == _factory.CreatedGameStatisticId && p.PlayerId == _factory.PlayersFactory.Player1Id);
		//
		// 	player1Stats.Wins.Should().Be(0);
		// 	player1Stats.AverageScore.Should().Be(22);
		//
		// 	var player2Stats = Context.PlayersStatistics.First(p =>
		// 		p.GameId == _factory.CreatedGameStatisticId && p.PlayerId == _factory.PlayersFactory.Player2Id);
		//
		// 	player2Stats.Wins.Should().Be(2);
		// 	player2Stats.AverageScore.Should().Be(39);
		// }
	}
}