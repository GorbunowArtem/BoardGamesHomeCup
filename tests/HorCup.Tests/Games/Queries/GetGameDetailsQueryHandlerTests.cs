using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games.Queries.GetDetails;
using HorCup.Presentation.Services.Games;
using HorCup.Tests.Games.Factory;
using HorCup.Tests.GamesStatistic.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Games.Queries
{
	public class GetGameDetailsQueryHandlerTests: TestFixtureBase
	{
		private readonly GamesStatisticsFactory _factory = new();
		private GetGameDetailsQueryHandler _sut;

		[SetUp]
		public void SetUp()
		{
			var gameServiceMock = new Mock<IGamesService>();
			gameServiceMock.Setup(gs => gs.TryGetGameAsync(_factory.GamesFactory.NotExistingGameId, default))
				.Throws<NotFoundException>();

			gameServiceMock.Setup(gs => gs.TryGetGameAsync(_factory.GamesFactory.Game4Id, default))
				.Returns(Task.FromResult(_factory.GamesFactory.Games.First(g => g.Id == _factory.GamesFactory.Game4Id)));

			gameServiceMock.Setup(gs => gs.TryGetGameAsync(_factory.GamesFactory.Game1Id, default))
				.Returns(Task.FromResult(_factory.GamesFactory.Games.First(g => g.Id == _factory.GamesFactory.Game1Id)));
			
			_sut = new GetGameDetailsQueryHandler(Context, NullLogger<GetGameDetailsQueryHandler>.Instance,
				gameServiceMock.Object);
		}

		[Test]
		public async Task Handle_GameDoesNotExists_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(new GetGameDetailsQuery(_factory.GamesFactory.NotExistingGameId), default))
				.Should()
				.ThrowAsync<NotFoundException>();
		}

		[Test]
		public async Task Handle_GameHasNoStatistic_GameWithDefaultStatisticReturned()
		{
			var details = await _sut.Handle(new GetGameDetailsQuery(_factory.GamesFactory.Game4Id), default);

			details.AverageScore.Should().Be(0);
			details.TimesPlayed.Should().Be(0);
			details.LastPlayedDate.Should().BeNull();
			details.Id.Should().Be(_factory.GamesFactory.Game4Id);
			details.Title.Should().Be(GamesFactory.Game4Title);
			details.MaxPlayers.Should().Be(GamesFactory.Game4MaxPlayers);
			details.MaxPlayers.Should().Be(GamesFactory.Game4MaxPlayers);
		}

		[Test]
		public async Task Handle_GameHasStatistic_GameWithStatisticReturned()
		{
			var details = await _sut.Handle(new GetGameDetailsQuery(_factory.GamesFactory.Game1Id), default);

			details.AverageScore.Should().Be(GamesStatisticsFactory.Game1AvgScore);
			details.TimesPlayed.Should().Be(GamesStatisticsFactory.Game1TimesPlayed);
			details.LastPlayedDate.Should().Be(_factory.Game1LastPlayedDate);
			details.Id.Should().Be(_factory.GamesFactory.Game1Id);
			details.Title.Should().Be(GamesFactory.Game1Title);
			details.MaxPlayers.Should().Be(GamesFactory.Game1MaxPlayers);
			details.MaxPlayers.Should().Be(GamesFactory.Game1MaxPlayers);
		}
	}
}