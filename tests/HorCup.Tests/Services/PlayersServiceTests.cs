using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Services.Players;
using HorCup.Tests.Players.Factory;
using NUnit.Framework;

namespace HorCup.Tests.Services
{
	[TestFixture]
	public class PlayersServiceTests : TestFixtureBase
	{
		private readonly PlayersService _sut;

		public PlayersServiceTests()
		{
			_sut = new PlayersService(Context);
		}

		[TestCase(PlayersFactory.Player2NickName, false)]
		[TestCase("newnickname", true)]
		public async Task IsNicknameUniqueAsync(string nickname, bool result)
		{
			var isUnique = await _sut.IsNicknameUniqueAsync(nickname.ToLowerInvariant());

			isUnique.Should().Be(result);
		}
	}
}