using System;
using System.Linq;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
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

			var query = _context.Players.Where(p =>
				p.Nickname.ToUpper().Equals(nickname.Trim().ToUpper()));

			if (id.HasValue)
			{
				query = query.Where(p => p.Id != id);
			}

			return await query.SingleOrDefaultAsync() == null;
		}
	}
}