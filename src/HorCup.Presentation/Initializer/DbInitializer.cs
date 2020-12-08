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
			new List<Game>
			{
				new Game
				{
					Title = "Эверделл",
					MinPlayers = 2,
					MaxPlayers = 4
				},
				new Game
				{
					Title = "Пэчворк",
					MinPlayers = 2,
					MaxPlayers = 2
				},
				new Game
				{
					Title = "Брасс Ланкашир",
					MinPlayers = 2,
					MaxPlayers = 4
				}
			}.Union(
			Enumerable.Range(1, 16)
				.Select(i => new Game
				{
					Title = $"Game {i} with title more than 20 symbols",
					MaxPlayers = i + 2,
					MinPlayers = ++i
				}));
		
		
	}
}