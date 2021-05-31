// using System.Linq;
// using System.Threading.Tasks;
// using FluentAssertions;
// using HorCup.Presentation.PlayersStatistic;
// using HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats;
// using Microsoft.Extensions.Logging.Abstractions;
// using NUnit.Framework;
//
// namespace HorCup.Tests.PlayersStatistic.Queries
// {
// 	public class SearchPlayerStatsQueryHandlerTests : TestFixtureBase
// 	{
// 		private SearchPlayerStatsQueryHandler _sut;
// 		private PlayersStatisticFactory _factory;
//
// 		[SetUp]
// 		public void SetUp()
// 		{
// 			_factory = new PlayersStatisticFactory();
// 			_sut = new SearchPlayerStatsQueryHandler(Context, Mapper, NullLogger<SearchPlayerStatsQueryHandler>.Instance);
// 		}
//
// 		[Test]
// 		public async Task Handle_SearchByGameId()
// 		{
// 			var (items, _) = await _sut.Handle(new SearchPlayerStatsQuery
// 			{
// 				GameId = _factory.GamesFactory.Game3Id,
// 				Take = 1
// 			}, default);
//
// 			items.Single().GameId.Should().Be(_factory.GamesFactory.Game3Id);
// 		}
//
// 		[Test]
// 		public async Task Handle_SearchByPlayerId()
// 		{
// 			var (items, _) = await _sut.Handle(new SearchPlayerStatsQuery
// 			{
// 				// PlayerId = _factory.PlayersFactory.Player2Id,
// 				Take = 1
// 			}, default);
//
// 			// items.Single().PlayerId.Should().Be(_factory.PlayersFactory.Player2Id);
// 		}
//
// 		[Test]
// 		public async Task Handle_SkipAndTake()
// 		{
// 			var (items, total) = await _sut.Handle(new SearchPlayerStatsQuery
// 			{
// 				Take = 1,
// 				Skip = 3
// 			}, default);
//
// 			var stat = items.Single();
// 			
// 			// stat.PlayerId.Should().Be(_factory.PlayersFactory.Player1Id);
// 			stat.GameId.Should().Be(_factory.GamesFactory.Game1Id);
// 			total.Should().Be(4);
// 		}
//
// 		[TestCase(PlayerStatsSortBy.AverageScore, 2, 431)]
// 		[TestCase(PlayerStatsSortBy.TotalPlayed, 11, 567)]
// 		[TestCase(PlayerStatsSortBy.TotalWins, 11, 431)]
// 		public async Task Handle_SortBy(PlayerStatsSortBy sortBy, int gameId, int playerId)
// 		{
// 			var (items, _) = await _sut.Handle(new SearchPlayerStatsQuery
// 			{
// 				SortBy = sortBy
// 			}, default);
//
// 			var stat = items.First();
// 			stat.GameId.Should().Be(gameId.Guid());
// 			stat.PlayerId.Should().Be(playerId.Guid());
// 		}
// 	}
// }