using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Players.Services.Players;
using HorCup.Tests.Base;
using NUnit.Framework;

namespace HorCup.Players.Tests.Services
{
	[TestFixture]
	public class PlayersServiceTests : TestFixtureBase
	{
		private readonly PlayersService _sut;

		public PlayersServiceTests()
		{
			_sut = new PlayersService(Context);
		}

		[TestCase("Player2Nick", 567, true)]
		[TestCase("Player2Nick", 123, false)]
		[TestCase("Player2Nick", null, false)]
		[TestCase("newnickname", null, true)]
		[TestCase("", null, true)]
		[TestCase(" ", null, true)]
		[TestCase(null, null, true)]
		public async Task IsNicknameUniqueAsync(string nickname, int? id, bool result)
		{
			var isUnique = await _sut.IsNicknameUniqueAsync($"  {nickname}  ", id.Guid());

			isUnique.Should().Be(result);
		}
	}
}