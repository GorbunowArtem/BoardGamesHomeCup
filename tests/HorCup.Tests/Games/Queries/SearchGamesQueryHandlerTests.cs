using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Games.Queries.SearchGames;
using HorCup.Tests.Games.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Games.Queries
{
	[TestFixture]
	public class SearchGamesQueryHandlerTests : TestFixtureBase
	{
		private SearchGamesQueryHandler _sut;
		private GamesFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new GamesFactory();
			_sut = new SearchGamesQueryHandler(Context, Mapper, NullLogger<SearchGamesQueryHandler>.Instance);
		}

		[TestCase("   AME 4   ", null, null, new[] {4})]
		[TestCase(null, 4, 8, new[] {3, 4})]
		[TestCase(null, 4, null, new[] {3, 4})]
		[TestCase(null, null, 6, new[] {2, 3})]
		public async Task Handle_SearchByParameters(
			string searchText,
			int? minPlayers,
			int? maxPlayers,
			int[] resultIds)
		{
			var result = await _sut.Handle(new SearchGamesQuery
			{
				MaxPlayers = maxPlayers,
				MinPlayers = minPlayers,
				SearchText = searchText
			}, CancellationToken.None);

			result.items.Select(g => g.Id).Should().BeEquivalentTo(ToGuidList(resultIds));
		}

		[Test]
		public async Task Handle_SkipAndTake()
		{
			var (items, total) = await _sut.Handle(new SearchGamesQuery
			{
				Skip = 1,
				Take = 1
			}, CancellationToken.None);

			total.Should().Be(3);
			items.First().Id.Should().Be(_factory.Game3Id);
		}
	}
}