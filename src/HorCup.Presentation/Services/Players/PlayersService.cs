using System;
using System.Linq;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.Players;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Services.Players
{
	public class PlayersService : IPlayersService
	{
		private readonly IHorCupContext _context;

		public PlayersService(IHorCupContext context)
		{
			_context = context;
		}

		public async Task<bool> IsNicknameUniqueAsync(string nickname, Guid? id)
		{
			if (string.IsNullOrEmpty(nickname) || string.IsNullOrWhiteSpace(nickname))
			{
				return true;
			}

			Player player;

			if (id.HasValue)
			{
				player = await _context.Players.Where(p =>
						p.Nickname.ToUpper().Contains(nickname.Trim().ToUpper()) && id != p.Id)
					.FirstOrDefaultAsync();
			}
			else
			{
				player = await _context.Players.Where(p =>
						p.Nickname.ToUpper().Contains(nickname.Trim().ToUpper()))
					.FirstOrDefaultAsync();
			}

			return player == null;
		}
	}
}