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

		[TestCase("game 2", false)]
		[TestCase("unique title", true)]
		[TestCase("", true)]
		[TestCase(null, true)]
		public async Task IsTitleUniqueAsync(string title, bool result)
		{
			var isUnique = await _sut.IsTitleUniqueAsync($"   {title}   ");

			isUnique.Should().Be(result);
		}
	}
}