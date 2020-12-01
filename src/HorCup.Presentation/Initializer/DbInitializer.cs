using System;
using System.Collections.Generic;
using System.Linq;
using HorCup.Presentation.Context;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;

namespace HorCup.Presentation.Initializer
{
	public class DbInitializer
	{
		public static void Initialize(HorCupContext context)
		{
			context.Database.EnsureDeleted();
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

		private static IEnumerable<Player> GetPlayers() =>
			Enumerable.Range(1, 15)
				.Select(i => new Player
				{
					Added = DateTime.Now,
					Nickname = $"player{i}",
					BirthDate = new DateTime(1990, 2, i),
					FirstName = $"Player{i}",
					LastName = $"Player{i}"
				});

		private static IEnumerable<Game> GetGames() =>
			Enumerable.Range(1, 16)
				.Select(i => new Game
				{
					Title = $"Game {i}",
					MaxPlayers = ++i
				});
	}
}