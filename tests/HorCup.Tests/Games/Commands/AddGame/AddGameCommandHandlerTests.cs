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

namespace HorCup.Tests.Games.Commands.AddGame
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
			idGeneratorMock.Setup(id => id.NewGuid()).Returns(_factory.CreatedGameId);


			var gamesService = new Mock<IGamesService>();
			gamesService.Setup(gs => gs.IsTitleUniqueAsync(GamesFactory.NotUniqueGameTitle, null, default))
				.Returns(Task.FromResult(false));

			var dateTimeServiceMock = new Mock<IDateTimeService>();
			
			gamesService.Setup(gs => gs.IsTitleUniqueAsync(GamesFactory.CreatedGameTitle, null, default))
				.Returns(Task.FromResult(true));

			_sut = new AddGameCommandHandler(Context,
				NullLogger<AddGameCommandHandler>.Instance,
				gamesService.Object,
				dateTimeServiceMock.Object,
				idGeneratorMock.Object);
		}

		[Test]
		public async Task Handle_GameCommandCorrect_GameAdded()
		{
			var id = await _sut.Handle(_factory.Commands.AddGameCommand(), CancellationToken.None);

			id.Should().Be(_factory.CreatedGameId);
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