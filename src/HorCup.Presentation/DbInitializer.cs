using System;
using System.Collections.Generic;
using System.Linq;
using HorCup.Presentation.Context;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;

namespace HorCup.Presentation
{
	internal static class DbInitializer
	{
		public static void Initialize(HorCupContext context)
		{
				context.Database.EnsureCreated();

				if (!context.Players.Any())
				{
					var games = GetGames();
					var players = GetPlayers();

					context.Players.AddRange(players);
					context.Games.AddRange(games);

					AddPlays(players, games, context);
					
					context.SaveChanges();
				}
		}

		private static void AddPlays(
			Player[] players,
			Game[] games,
			HorCupContext context)
		{
			
			for (int i = 0; i < 40; i++)
			{
				
				var id = Guid.NewGuid();

				
				context.Plays.Add(new Play
				{
					Id = id,
					Notes = $"Note {i}",
					GameId = games[i].Id,
					PlayedDate = DateTime.Now.AddDays(-i),
				});
				
				var score = new PlayScore[]
				{
					new()
					{
						PlayId = id,
						Score = 12 + i,
						PlayerId = players[i].Id,
					},
					new()
					{
						PlayId = id,
						Score = 22 + i,
						PlayerId = players[i + 1].Id,
						IsWinner = true
					}
				};
				
				context.PlayScores.AddRange(score);

				context.SaveChanges();
				
			}
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

		private static Player[] GetPlayers() =>
			Enumerable.Range(1,50).Select(i => new Player
			{
				Added = DateTime.Now.AddDays(-i),
				Nickname = $"Игрок{i} Игроков",
			}).ToArray();
	}
}