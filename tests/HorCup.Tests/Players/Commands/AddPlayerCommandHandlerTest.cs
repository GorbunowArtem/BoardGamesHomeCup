using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.Services.DateTimeService;
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
		private AddPlayerCommandHandler _sut;
		private PlayersFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new PlayersFactory();

			var idGeneratorServiceMock = new Mock<IIdGenerator>();
			idGeneratorServiceMock.Setup(id => id.NewGuid()).Returns(new Guid());

			var dateTimeServiceMock = new Mock<IDateTimeService>();
			dateTimeServiceMock.Setup(dt => dt.Now).Returns(new DateTime());

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
				playersServiceMock.Object,
				dateTimeServiceMock.Object);
		}

		[Test]
		public async Task Handle_PlayerModelCorrect_PlayerAdded()
		{
			var player = await _sut.Handle(_factory.Commands.AddPlayer(), CancellationToken.None);

			player.BirthDate.Should().Be(_factory.Player3BirthDate);
			player.FirstName.Should().Be(PlayersFactory.Player3FirstName.Trim());
			player.LastName.Should().Be(PlayersFactory.Player3LastName.Trim());
			player.Nickname.Should().Be(PlayersFactory.Player3NickName.Trim());
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