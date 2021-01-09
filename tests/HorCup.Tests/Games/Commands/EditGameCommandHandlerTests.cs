using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.Games.Commands.EditGame;
using HorCup.Presentation.Services.Games;
using HorCup.Tests.Games.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Games.Commands
{
	public class EditGameCommandHandlerTests : TestFixtureBase
	{
		private readonly GamesFactory _factory = new();
		private EditGameCommandHandler _sut;

		[SetUp]
		public void SetUp()
		{
			var gamesServiceMock = new Mock<IGamesService>();
			gamesServiceMock.Setup(g => g.IsTitleUniqueAsync(GamesFactory.UpdatedTitle, _factory.Game2Id))
				.Returns(Task.FromResult(true));

			gamesServiceMock.Setup(g => g.IsTitleUniqueAsync(GamesFactory.Game3Title, _factory.Game3Id))
				.Returns(Task.FromResult(false));

			_sut = new EditGameCommandHandler(Context, gamesServiceMock.Object,
				NullLogger<EditGameCommandHandler>.Instance);
		}

		[Test]
		public async Task Handle_GameUpdated()
		{
			var updatedGame = _factory.Commands.EditGameCommand;
			
			await _sut.Handle(updatedGame, CancellationToken.None);

			var game = Context.Games.First(g => g.Id == _factory.Game2Id);

			game.Title.Should().Be(updatedGame.Title);
			game.MinPlayers.Should().Be(updatedGame.MinPlayers);
			game.MaxPlayers.Should().Be(updatedGame.MaxPlayers);
		}
		
		[Test]
		public async Task Handle_TitleNotUnique_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(
						_factory.Commands.EditGameCommand with {Id = _factory.Game3Id, Title = GamesFactory.Game3Title},
						CancellationToken.None))
				.Should()
				.ThrowAsync<EntityExistsException>()
				.WithMessage($"Entity {nameof(Game)} with {_factory.Game3Id.ToString()} already exists");
		}

		[Test]
		public async Task Handle_GameNotExists_ExceptionThrown()
		{
			var id = new Guid();

			await _sut.Invoking(handler =>
					handler.Handle(_factory.Commands.EditGameCommand with {Id = id}, CancellationToken.None))
				.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage($"Entity {nameof(Game)} with key {id} was not found");
		}
	}
}