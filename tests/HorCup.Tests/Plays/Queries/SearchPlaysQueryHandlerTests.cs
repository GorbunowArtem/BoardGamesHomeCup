using HorCup.Presentation.Plays.Queries.SearchPlays;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Plays.Queries
{
	public class SearchPlaysQueryHandlerTests: TestFixtureBase
	{
		private SearchPlaysQueryHandler _sut;
		
		[SetUp]
		public void SetUp()
		{
			_sut = new SearchPlaysQueryHandler(Context, NullLogger<SearchPlaysQueryHandler>.Instance);
		}
	}
}