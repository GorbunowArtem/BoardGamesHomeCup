using System;
using System.Linq;
using HorCup.Games.Context;
using HorCup.Games.Models;

namespace HorCup.Games
{
	internal static class DbInitializer
	{
		public static void Initialize(GamesContext context)
		{
				context.Database.EnsureCreated();

				if (!context.Games.Any())
				{
					context.Games.AddRange(GetGames());
					context.SaveChanges();
				}
		}

		private static Game[] GetGames() =>
			Enumerable.Range(1, 5000)
				.Select(i => new Game
				{
				 Added = DateTime.Now.AddDays(-i),
				 Title = $"Игра {i}",
				 MaxPlayers = 6,
				 MinPlayers = 2
				}).ToArray();

	}
}