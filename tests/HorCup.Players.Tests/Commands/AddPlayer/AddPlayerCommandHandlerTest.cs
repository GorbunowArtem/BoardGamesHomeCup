using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Infrastructure.Exceptions;
using HorCup.Infrastructure.Services.DateTimeService;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Players.Commands.AddPlayer;
using HorCup.Players.Models;
using HorCup.Players.Services.Players;
using HorCup.Players.Tests.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Players.Tests.Commands.AddPlayer;

public class AddPlayerCommandHandlerTest : TestFixtureBase
{
	private AddPlayerCommandHandler _sut;
	private PlayersFactory _factory;

	[SetUp]
	public void SetUp()
	{
		_factory = new PlayersFactory();

		var idGeneratorServiceStub = new Mock<IIdGenerator>();
		idGeneratorServiceStub.Setup(id => id.NewGuid()).Returns(_factory.CreatedPlayerId);

		var dateTimeServiceStub = new Mock<IDateTimeService>();
		dateTimeServiceStub.Setup(dt => dt.UtcNow).Returns(new DateTime(2020, 12, 12));

		var playersServiceStub = new Mock<IPlayersService>();
		playersServiceStub.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player1NickName, null))
			.Returns(Task.FromResult(false));

		playersServiceStub.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player3NickName, null))
			.Returns(Task.FromResult(true));

		_sut = new AddPlayerCommandHandler(
			Context,
			new NullLogger<AddPlayerCommandHandler>(),
			playersServiceStub.Object,
			dateTimeServiceStub.Object,
			idGeneratorServiceStub.Object);
	}

	[Test]
	public async Task Handle_PlayerModelCorrect_PlayerAdded()
	{
		var player = await _sut.Handle(_factory.Commands.AddPlayer(), CancellationToken.None);

		player.Should().Be(_factory.CreatedPlayerId);
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