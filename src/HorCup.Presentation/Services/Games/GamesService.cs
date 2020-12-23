using System;
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

		public async Task<bool> IsTitleUniqueAsync(string title)
		{
			if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
			{
				return true;
			}

			var gameWithSameTitleCount =
				await _context.Games.CountAsync(g => g.Title.ToUpper()
					.Contains(title.Trim().ToUpper()));

			return gameWithSameTitleCount == 0;
		}
	}
}