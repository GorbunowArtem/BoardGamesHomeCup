using System;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Services.Players;
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

		[TestCase("player2nick", null, false)]
		[TestCase("newnickname", null, true)]
		[TestCase("", null, true)]
		[TestCase(null, null, true)]
		public async Task IsNicknameUniqueAsync(string nickname, Guid? id, bool result)
		{
			var isUnique = await _sut.IsNicknameUniqueAsync($"  {nickname}  ", id);

			isUnique.Should().Be(result);
		}
	}
}