using System.Threading.Tasks;
using AutoFixture;
using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Plays.Commands.AddPlay;
using HorCup.Plays.Models;
using HorCup.Plays.Shared.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Plays.Tests.Commands.AddPlay;

[TestFixture]
public class AddPlayCommandHandlerTests : TestFixtureBase
{
	private AddPlayCommandHandler _sut;

	[SetUp]
	public void SetUp()
	{
		var idGeneratorMock = new Mock<IIdGenerator>();

		_sut = new AddPlayCommandHandler(idGeneratorMock.Object, Context.Object,
			NullLogger<AddPlayCommandHandler>.Instance, Mapper, PublishEndpoint.Object);
	}

	[Test]
	public async Task Handle_NewPlay_PlayAdded()
	{
		await _sut.Handle(new Fixture().Create<AddPlayCommand>(), default);

		Context.Verify(c => c.Plays.InsertOneAsync(It.IsAny<Play>(), null, default), Times.Once);
	}

	[Test]
	public async Task Handle_NewPlay_ShouldPublishGamePlayedIntegrationEvent()
	{
		var command = new Fixture().Create<AddPlayCommand>();

		await _sut.Handle(command, default);
			
		PublishEndpoint.Verify(e => e.Publish(It.IsAny<GamePlayed>(), default));
	}
}