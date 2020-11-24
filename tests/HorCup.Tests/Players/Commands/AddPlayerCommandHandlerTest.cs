using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Presentation.Services.Players;
using HorCup.Tests.Players.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Players.Commands
{
	public class AddPlayerCommandHandlerTest : TestFixtureBase
	{
		private readonly AddPlayerCommandHandler _sut;
		private readonly PlayersFactory _factory;

		public AddPlayerCommandHandlerTest()
		{
			_factory = new PlayersFactory();

			var idGeneratorServiceMock = new Mock<IIdGenerator>();
			idGeneratorServiceMock.Setup(id => id.NewGuid()).Returns(new Guid());

			var playersServiceMock = new Mock<IPlayersService>();
			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player1NickName))
				.Returns(Task.FromResult(false));
			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player3NickName))
				.Returns(Task.FromResult(true));

			_sut = new AddPlayerCommandHandler(
				Context,
				idGeneratorServiceMock.Object,
				Mapper,
				new NullLogger<AddPlayerCommandHandler>(),
				playersServiceMock.Object);
		}

		[Test]
		public async Task Handle_PlayerModelCorrect_PlayerAdded()
		{
			var player = await _sut.Handle(_factory.Commands.AddPlayer(), CancellationToken.None);

			player.BirthDate.Should().Be(_factory.Player3BirthDate);
			player.FirstName.Should().Be(PlayersFactory.Player3FirstName);
			player.LastName.Should().Be(PlayersFactory.Player3LastName);
			player.Nickname.Should().Be(PlayersFactory.Player3NickName);
		}

		[Test]
		public async Task Handle_NicknameOccupied_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(_factory.Commands.AddPlayer(PlayersFactory.Player1NickName), CancellationToken.None))
				.Should()
				.ThrowAsync<EntityExistsException>()
				.WithMessage($"Entity {nameof(Player)} with {PlayersFactory.Player1NickName} already exists");
		}
	}
}