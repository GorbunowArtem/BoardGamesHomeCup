using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Infrastructure.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.Plays.Commands.AddPlay;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Tests.Plays.Factory;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Plays.Commands.AddPlay
{
	public class AddPlayCommandHandlerTests : TestFixtureBase
	{
		private AddPlayCommandHandler _sut;
		private PlaysFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new PlaysFactory();

			var iidGeneratorMock = new Mock<IIdGenerator>();
			iidGeneratorMock.Setup(g => g.NewGuid()).Returns(_factory.CreatedPlay1Id);

			_sut = new AddPlayCommandHandler(iidGeneratorMock.Object, Context,
				NullLogger<AddPlayCommandHandler>.Instance,
				new Mock<IPublisher>().Object);
		}

		[Test]
		public async Task Handle_PlayAdded()
		{
			var id = await _sut.Handle(_factory.Commands.AddPlayCommand, CancellationToken.None);

			id.Should().Be(_factory.CreatedPlay1Id);
		}

		[Test]
		public async Task Handle_GameNotExists_ExceptionThrown()
		{
			var id = new Guid();

			await _sut.Invoking(handler =>
					handler.Handle(_factory.Commands.AddPlayCommand with{ GameId = id}, CancellationToken.None))
				.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage($"Entity {nameof(Game)} with key {id.ToString()} was not found");
		}

		[Test]
		public async Task Handle_GameMinPlayersIsBiggerThanPlayersCount_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(
						_factory.Commands.AddPlayCommand with{PlayerScores = PlaysFactory.Play1Scores.Take(2)},
						CancellationToken.None))
				.Should()
				.ThrowAsync<ArgumentException>()
				.WithMessage("Players count cannot be less than game minimum players");
		}

		[Test]
		public async Task Handle_GameMaxPlayersIsLessThanPlayersCount_ExceptionThrown()
		{
			await _sut.Invoking(handler =>
					handler.Handle(
						_factory.Commands.AddPlayCommand with{PlayerScores =
							PlaysFactory.Play2Scores}, CancellationToken.None))
				.Should()
				.ThrowAsync<ArgumentException>()
				.WithMessage("Players count cannot be bigger than game maximum players");
		}
	}
}