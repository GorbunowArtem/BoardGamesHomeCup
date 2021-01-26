using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Players.Queries.SearchPlayers;
using HorCup.Tests.Players.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Players.Queries.SearchPlayers
{
	public class SearchPlayersQueryHandlerTests: TestFixtureBase
	{
		private SearchPlayersQueryHandler _sut;
		private PlayersFactory _factory;

		[SetUp]
		public void SetUp()
		{
			_factory = new PlayersFactory();
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
		public async Task Handle_ExceptIds()
		{
			var (items, total) = await _sut.Handle(new SearchPlayersQuery
			{
				Skip = 0,
				Take = 10,
				ExceptIds = new []
				{
					_factory.Player1Id
				} 
			}, CancellationToken.None);

			items.First().Id.Should().Be(_factory.Player2Id);
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