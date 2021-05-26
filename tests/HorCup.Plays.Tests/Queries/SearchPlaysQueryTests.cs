using System;
using System.Threading.Tasks;
using HorCup.Plays.Models;
using HorCup.Plays.Queries.SearchPlays;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using Moq;
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
		[Ignore("Needs a lot of time to setup mocks")]
		public async Task Handle_ShouldFilterByGames()
		{
			Context.Setup(p => p.Plays).Returns(new Mock<IMongoCollection<Play>>().Object);

			var result = await _sut.Handle(new SearchPlaysQuery
			{
				Take = 10                ,
				GamesIds = new []
				{
					Guid.NewGuid(), 
				}
			}, default);
			
		}
	}
}