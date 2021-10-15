using System;
using System.Linq;
using HorCup.Players.Context;
using HorCup.Players.Models;

namespace HorCup.Players
{
	internal static class DbInitializer
	{
		public static void Initialize(PlayersContext context)
		{
			context.Database.EnsureCreated();

			if (!context.Players.Any())
			{
				context.Players.AddRange(GetPlayers());

				context.SaveChanges();
			}
		}

		private static Player[] GetPlayers() =>
			Enumerable.Range(1, 5000)
				.Select(i => new Player
				{
					Added = DateTime.Now.AddDays(-i),
					Nickname = $"Игрок{i} Игроков",
				})
				.ToArray();
	}
}