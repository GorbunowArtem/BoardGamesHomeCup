using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.Games.Commands.AddGame;
using HorCup.Presentation.Services.DateTimeService;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Tests.Games.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Games.Commands
{
	[TestFixture]
	public class AddGameCommandHandlerTests : TestFixtureBase
	{
		private AddGameCommandHandler _sut;
		private GamesFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new GamesFactory();

			var idGeneratorMock = new Mock<IIdGenerator>();
			idGeneratorMock.Setup(id => id.NewGuid()).Returns(_factory.Game1Id);

			var gamesService = new Mock<IGamesService>();
			gamesService.Setup(gs => gs.IsTitleUniqueAsync(GamesFactory.NotUniqueGameTitle))
				.Returns(Task.FromResult(false));

			var dateTimeServiceMock = new Mock<IDateTimeService>();
			
			
			gamesService.Setup(gs => gs.IsTitleUniqueAsync(GamesFactory.Game1Title))
				.Returns(Task.FromResult(true));

			_sut = new AddGameCommandHandler(Context,
				idGeneratorMock.Object,
				Mapper,
				NullLogger<AddGameCommandHandler>.Instance,
				gamesService.Object,
				dateTimeServiceMock.Object);
		}

		[Test]
		public async Task Handle_GameCommandCorrect_GameAdded()
		{
			var game = await _sut.Handle(_factory.Commands.AddGameCommand(), CancellationToken.None);

			game.Title.Should().Be(GamesFactory.Game1Title);
			game.MaxPlayers.Should().Be(GamesFactory.Game1MaxPlayers);
			game.MinPlayers.Should().Be(GamesFactory.Game1MinPlayers);
			game.Id.Should().Be(_factory.Game1Id);
		}

		[Test]
		public async Task Handle_GameWithSameTitleExists_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(_factory.Commands.AddGameCommand(GamesFactory.NotUniqueGameTitle), CancellationToken.None))
				.Should()
				.ThrowAsync<EntityExistsException>()
				.WithMessage($"Entity {nameof(Game)} with {GamesFactory.NotUniqueGameTitle} already exists");
		}
	}
}