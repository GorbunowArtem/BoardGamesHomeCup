using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Services.Games;
using NUnit.Framework;

namespace HorCup.Tests.Services
{
	[TestFixture]
	public class GamesServiceTest: TestFixtureBase
	{
		private GamesService _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new GamesService(Context);
		}

		[TestCase("game 2", 324, false)]
		[TestCase("game 2", 2, true)]
		[TestCase("game 2", null, false)]
		[TestCase("unique title", null, true)]
		[TestCase("", null, true)]
		[TestCase(null, null, true)]
		public async Task IsTitleUniqueAsync(string title, int id, bool result)
		{
			var isUnique = await _sut.IsTitleUniqueAsync($"   {title}   ", id.Guid(), default);

			isUnique.Should().Be(result);
		}
	}
}