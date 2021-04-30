// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using HorCup.Infrastructure.Exceptions;
// using HorCup.Players.Models;
// using HorCup.Players.Players.Queries.GetById;
// using Microsoft.Extensions.Logging.Abstractions;
// using NUnit.Framework;
//
// namespace HorCup.Players.Tests.Players.Queries
// {
// 	[TestFixture]
// 	public class GetPlayerByIdQueryHandlerTests: TestFixtureBase
// 	{
// 		private GetPlayerByIdQueryHandler _sut;
// 		
// 		[SetUp]
// 		public void SetUp()
// 		{
// 			_sut = new GetPlayerByIdQueryHandler(Context, NullLogger<GetPlayerByIdQueryHandler>.Instance, Mapper);	
// 		}
// 		
// 		[Test]
// 		public async Task Handle_Player_exists_PlayerDetailsReturned()
// 		{
// 			var playerId = Context.Players.First().Id;
//
// 			var player = await _sut.Handle(new GetPlayerByIdQuery(playerId), CancellationToken.None);
//
// 			player.Should().NotBeNull();
// 		}
//
// 		[Test]
// 		public async Task Handle_PlayerDoesNotExists_ExceptionThrown()
// 		{
// 			var notExistingId = Guid.NewGuid();
// 			
// 			await _sut.Invoking(handler =>
// 					handler.Handle(new GetPlayerByIdQuery(notExistingId), CancellationToken.None))
// 				.Should()
// 				.ThrowAsync<NotFoundException>()
// 				.WithMessage($"Entity {nameof(Player)} with key {notExistingId} was not found");
// 		}
// 	}
// }