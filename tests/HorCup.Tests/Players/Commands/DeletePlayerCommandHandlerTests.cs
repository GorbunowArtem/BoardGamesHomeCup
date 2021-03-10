using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Infrastructure.Exceptions;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.DeletePlayer;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Players.Commands
{
	[TestFixture]
	public class DeletePlayerCommandHandlerTests: TestFixtureBase
	{
		private DeletePlayerCommandHandler _sut;
		
		[SetUp]
		public void SetUp()
		{
			_sut = new DeletePlayerCommandHandler(Context, NullLogger<DeletePlayerCommandHandler>.Instance);	
		}

		[Test]
		public async Task Handle_PlayerExists_PlayerDeleted()
		{
			var playerId = Context.Players.First().Id;

			await _sut.Handle(new DeletePlayerCommand(playerId), CancellationToken.None);

			var player = Context.Players.FirstOrDefault(p => p.Id == playerId);

			player.Should().BeNull();
		}

		[Test]
		public async Task Handle_PlayerDoesNotExists_ExceptionThrown()
		{
			var notExistingPlayerId = new Guid();
			
			await _sut.Invoking(handler => handler.Handle(new DeletePlayerCommand(notExistingPlayerId), CancellationToken.None))
				.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage($"Entity {nameof(Player)} with key {notExistingPlayerId} was not found");
		}
	}
}