using System;
using System.Linq;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Services.Games
{
	public class GamesService : IGamesService
	{
		private readonly IHorCupContext _context;

		public GamesService(IHorCupContext context)
		{
			_context = context;
		}

		public async Task<bool> IsTitleUniqueAsync(string title, Guid? id)
		{
			if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
			{
				return true;
			}

			var query = _context.Games.Where(g => g.Title.ToUpper()
				.Equals(title.Trim().ToUpper()));

			if (id.HasValue)
			{
				query = query.Where(g => g.Id != id);
			}

			return !await query.AnyAsync();
		}
	}
}