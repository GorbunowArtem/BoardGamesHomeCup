using System.Threading.Tasks;
using HorCup.Plays.Queries.SearchPlays;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Plays.Tests.Queries
{
	[TestFixture]
	public class SearchPlaysQueryTests: TestFixtureBase
	{
		private SearchPlaysQueryHandler _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new SearchPlaysQueryHandler(Context.Object, NullLogger<SearchPlaysQueryHandler>.Instance, Mapper);
		}

		[Test]
		public async Task Handle_ShouldFilterByGames()
		{
			
		}
	}
}