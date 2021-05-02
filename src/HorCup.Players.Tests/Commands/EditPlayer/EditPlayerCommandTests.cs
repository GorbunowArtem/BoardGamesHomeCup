using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Infrastructure.Exceptions;
using HorCup.Infrastructure.Services.DateTimeService;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Players.Commands.AddPlayer;
using HorCup.Players.Commands.EditPlayer;
using HorCup.Players.Services.Players;
using HorCup.Players.Tests.Factory;
using HorCup.Tests.Base;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Players.Tests.Commands.EditPlayer
{
	public class EditPlayerCommandTests: TestFixtureBase
	{
		private const string UpdateNickname = "Updated nickname";
		private EditPlayerCommandHandler _sut;
		private PlayersFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new PlayersFactory();

			var idGeneratorServiceMock = new Mock<IIdGenerator>();
			idGeneratorServiceMock.Setup(id => id.NewGuid()).Returns(_factory.CreatedPlayerId);

			var playersServiceMock = new Mock<IPlayersService>();
			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player1NickName, null))
				.Returns(Task.FromResult(false));
			
			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(UpdateNickname, _factory.Player2Id))
				.Returns(Task.FromResult(true));

			_sut = new EditPlayerCommandHandler(
				Context,
				new NullLogger<EditPlayerCommandHandler>(),
				playersServiceMock.Object);
		}

		[Test]
		public async Task Handle_PlayerNotExists_ExceptionThrown()
		{
			var notExistingPlayerCommand = new EditPlayerCommand(555.Guid(), string.Empty);

			await _sut.Invoking(handler => handler.Handle(notExistingPlayerCommand, default))
				.Should()
				.ThrowAsync<NotFoundException>();
		}

		[Test]
		public async Task Handle_PlayerWithSameNicknameExists_ExceptionThrown()
		{
			var duplicatePlayer = new EditPlayerCommand(_factory.Player2Id, PlayersFactory.Player3NickName);

			await _sut.Invoking(handler => handler.Handle(duplicatePlayer, default))
				.Should()
				.ThrowAsync<EntityExistsException>();
			
		}

		[Test]
		public async Task Handle_NickNameUnique_PlayerUpdated()
		{
			var editCommand = new EditPlayerCommand(_factory.Player2Id, UpdateNickname);

			await _sut.Handle(editCommand, default);

			var updated = Context.Players.First(p => p.Id == _factory.Player2Id);
			
			updated.Nickname.Should().Be(UpdateNickname);
		}
	}
}