using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Players.Queries.SearchPlayers;
using HorCup.Tests.Players.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Players.Queries
{
	public class SearchPlayersQueryHandlerTests: TestFixtureBase
	{
		private SearchPlayersQueryHandler _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new SearchPlayersQueryHandler(Context, NullLogger<SearchPlayersQueryHandler>.Instance, Mapper);
		}
		
		[TestCase("   1 Fir", PlayersFactory.Player1FirstName)]
		[TestCase("2Nick    ", PlayersFactory.Player2FirstName)]
		[TestCase("   2 La", PlayersFactory.Player2FirstName)]
		public async Task Handle_SearchByText(string searchText, string resultName)
		{
			var (items, total) = await _sut.Handle(new SearchPlayersQuery
			{
				Skip = 0,
				Take = 10,
				SearchText = searchText
			}, CancellationToken.None);

			items.First().FirstName.Should().Be(resultName);
			total.Should().Be(1);
		}

		[Test]
		public async Task Handle_SkipAndTakeEquals1_SecondPlayerReturned()
		{
			var (items, total) = await _sut.Handle(new SearchPlayersQuery
			{
				Skip = 1,
				Take = 1
			}, CancellationToken.None);

			items.First().FirstName.Should().Be(PlayersFactory.Player2FirstName);
			total.Should().Be(2);
		}
	}
}