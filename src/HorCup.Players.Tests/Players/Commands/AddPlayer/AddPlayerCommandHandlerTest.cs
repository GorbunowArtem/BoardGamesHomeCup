// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using FluentAssertions;
// using HorCup.Infrastructure.Exceptions;
// using HorCup.Infrastructure.Services.DateTimeService;
// using HorCup.Infrastructure.Services.IdGenerator;
// using HorCup.Players.Models;
// using HorCup.Players.Players.Commands.AddPlayer;
// using HorCup.Players.Services.Players;
// using HorCup.Players.Tests.Players.Factory;
// using HorCup.Tests;
// using Microsoft.Extensions.Logging.Abstractions;
// using Moq;
// using NUnit.Framework;
//
// namespace HorCup.Players.Tests.Players.Commands.AddPlayer
// {
// 	public class AddPlayerCommandHandlerTest : TestFixtureBase
// 	{
// 		private AddPlayerCommandHandler _sut;
// 		private PlayersFactory _factory;
//
// 		[SetUp]
// 		public void SetUp()
// 		{
// 			_factory = new PlayersFactory();
//
// 			var idGeneratorServiceMock = new Mock<IIdGenerator>();
// 			idGeneratorServiceMock.Setup(id => id.NewGuid()).Returns(_factory.CreatedPlayerId);
//
// 			var dateTimeServiceMock = new Mock<IDateTimeService>();
// 			dateTimeServiceMock.Setup(dt => dt.Now).Returns(new DateTime());
//
// 			var playersServiceMock = new Mock<IPlayersService>();
// 			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player1NickName, null))
// 				.Returns(Task.FromResult(false));
// 			
// 			playersServiceMock.Setup(ps => ps.IsNicknameUniqueAsync(PlayersFactory.Player3NickName, null))
// 				.Returns(Task.FromResult(true));
//
// 			_sut = new AddPlayerCommandHandler(
// 				Context,
// 				new NullLogger<AddPlayerCommandHandler>(),
// 				playersServiceMock.Object,
// 				dateTimeServiceMock.Object,
// 				idGeneratorServiceMock.Object);
// 		}
//
// 		[Test]
// 		public async Task Handle_PlayerModelCorrect_PlayerAdded()
// 		{
// 			var player = await _sut.Handle(_factory.Commands.AddPlayer(), CancellationToken.None);
//
// 			player.Should().Be(_factory.CreatedPlayerId);
// 		}
//
// 		[Test]
// 		public async Task Handle_NicknameOccupied_ExceptionThrown()
// 		{
// 			await _sut.Invoking(handler =>
// 					handler.Handle(_factory.Commands.AddPlayer(PlayersFactory.Player1NickName), CancellationToken.None))
// 				.Should()
// 				.ThrowAsync<EntityExistsException>()
// 				.WithMessage($"Entity {nameof(Player)} with {PlayersFactory.Player1NickName} already exists");
// 		}
// 	}
// }