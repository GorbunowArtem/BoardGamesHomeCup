using System;
using System.Collections.Generic;
using System.Linq;
using HorCup.Presentation.Context;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;

namespace HorCup.Presentation.Initializer
{
	public class DbInitializer
	{
		public static void Initialize(HorCupContext context)
		{
#if DEBUG
	//		context.Database.EnsureDeleted();
#endif
			context.Database.EnsureCreated();

			if (!context.Players.Any())
			{
				context.Players.AddRange(GetPlayers());
			}

			if (!context.Games.Any())
			{
				context.Games.AddRange(GetGames());
			}

			if (!context.Plays.Any())
			{
				context.Plays.AddRange(GetPlays());
				context.PlayScores.AddRange(GetPlayScores());
			}

			context.SaveChanges();
		}

		private static IEnumerable<Player> GetPlayers() =>
			new[]
			{
				new Player
				{
					Id = new Guid("2C55C3B6-94BE-4A80-8B96-39A2361132DC"),
					Added = DateTime.Now,
					Nickname = "AlreadyPlayed1",
					BirthDate = new DateTime(2000, 1, 1),
					FirstName = "Played 1",
					LastName = "Played 1"
				},
				new Player
				{
					Id = new Guid("48FD7BB9-87BA-4E93-8C59-06AF92AC14E3"),
					Added = DateTime.Now,
					Nickname = "AlreadyPlayed2",
					BirthDate = new DateTime(2000, 1, 1),
					FirstName = "Played 2",
					LastName = "Played 2"
				}, 
			}.Union(
			Enumerable.Range(1, 15)
				.Select(i => new Player
				{
					Id = Guid.NewGuid(),
					Added = DateTime.Now,
					Nickname = $"player{i}",
					BirthDate = new DateTime(1990, 2, i),
					FirstName = $"Player{i}",
					LastName = $"Player{i}"
				}));

		private static IEnumerable<Game> GetGames() =>
			new List<Game>
			{
				new Game
				{
					Id = new Guid("9A178137-5401-4063-8B65-7E005C426AFF"),
					Title = "Эверделл",
					MinPlayers = 2,
					MaxPlayers = 4,
					HasScores = true
				},
				new Game
				{
					Id = new Guid("A9636E89-D699-49C7-A77C-D7190008905A"),
					Title = "Пэчворк",
					MinPlayers = 2,
					MaxPlayers = 2,
					HasScores = true
				},
				new Game
				{
					Id = new Guid("61D87448-C511-4509-8B0E-57F7393EB3CE"),
					Title = "Брасс Ланкашир",
					MinPlayers = 2,
					MaxPlayers = 4,
					HasScores = true
				}
			}.Union(
			Enumerable.Range(1, 16)
				.Select(i => new Game
				{
					Title = $"Game {i} with title more than 20 symbols",
					MaxPlayers = i + 2,
					MinPlayers = ++i,
					HasScores = false
				}));
		
		private static IEnumerable<Play> GetPlays() =>
		new List<Play>
		{
			new()
			{
				Id = new Guid("77569BE9-BEE9-493C-B9CE-212B42738DE8"),
				GameId = new Guid("9A178137-5401-4063-8B65-7E005C426AFF"),
				PlayedDate = DateTime.Now,
				Notes = "Неплохо сыграли"
			}
		};

		private static IEnumerable<PlayScore> GetPlayScores() =>
			new[]
			{
				new PlayScore
				{
					PlayerId = new Guid("2C55C3B6-94BE-4A80-8B96-39A2361132DC"),
					Score = 22,
					IsWinner = false,
					PlayId = new Guid("77569BE9-BEE9-493C-B9CE-212B42738DE8") 
				},
				new PlayScore
				{
					PlayerId = new Guid("48FD7BB9-87BA-4E93-8C59-06AF92AC14E3"),
					Score = 32,
					IsWinner = true,
					PlayId =new Guid("77569BE9-BEE9-493C-B9CE-212B42738DE8") 
				},
			};
	}
}