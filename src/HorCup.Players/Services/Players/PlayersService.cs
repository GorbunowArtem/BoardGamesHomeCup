using System;
using System.Linq;
using System.Threading.Tasks;
using HorCup.Players.Context;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Services.Players
{
	public class PlayersService : IPlayersService
	{
		private readonly IPlayersContext _context;

		public PlayersService(IPlayersContext context)
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