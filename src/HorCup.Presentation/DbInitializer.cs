using System;
using System.Collections.Generic;
using System.Linq;
using HorCup.Presentation.Context;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;

namespace HorCup.Presentation
{
	internal static class DbInitializer
	{
		public static void Initialize(HorCupContext context)
		{
				context.Database.EnsureCreated();

				if (!context.Players.Any())
				{
					context.Players.AddRange(GetPlayers());
				}

				if (!context.Games.Any())
				{
					context.Games.AddRange(GetGames());
				}

				context.SaveChanges();
			}

		private static Game[] GetGames() =>
			Enumerable.Range(1, 50)
				.Select(i => new Game
				{
				 Added = DateTime.Now.AddDays(-i),
				 Title = $"Игра {i}",
				 MaxPlayers = 6,
				 MinPlayers = 2
				}).ToArray();

		private static IEnumerable<Player> GetPlayers()
			{
				return Enumerable.Range(1,50).Select(i => new Player
				{
					Added = DateTime.Now.AddDays(-i),
					Nickname = $"Игрок{i} Игроков",
				});
			}	
	}
}