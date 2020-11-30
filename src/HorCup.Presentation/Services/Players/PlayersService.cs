using System.Linq;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Services.Players
{
	public class PlayersService: IPlayersService
	{
		private readonly IHorCupContext _context;

		public PlayersService(IHorCupContext context)
		{
			_context = context;
		}

		public async Task<bool> IsNicknameUniqueAsync(string nickname)
		{
			var playerWithSameNickName = await _context.Players.Where(p =>
				p.Nickname.ToUpper().Contains(nickname.ToUpper()))
				.FirstOrDefaultAsync();

			return playerWithSameNickName == null;
		}
	}
}