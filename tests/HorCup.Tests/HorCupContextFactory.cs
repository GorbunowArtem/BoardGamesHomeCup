using System;
using HorCup.Presentation.Context;
using HorCup.Tests.Games.Factory;
using HorCup.Tests.GamesStatistic.Factory;
using HorCup.Tests.Players.Factory;
using HorCup.Tests.Plays.Factory;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Tests
{
	public static class HorCupContextFactory
	{
		public static HorCupContext Create()
		{
			var options = new DbContextOptionsBuilder<HorCupContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new HorCupContext(options);

			context.Database.EnsureCreated();

			var playersFactory = new PlayersFactory();
			context.Players.AddRange(playersFactory.Players);

			var gamesFactory = new GamesFactory();
			context.Games.AddRange(gamesFactory.Games);

			var playsFactory = new PlaysFactory();
			context.Plays.AddRange(playsFactory.Plays);
			context.PlayScores.AddRange(playsFactory.PlayScores);

			var gameStatFactory = new GamesStatisticsFactory();
			context.GamesStatistics.AddRange(gameStatFactory.GameStatistics);
			
			context.SaveChanges();

			return context;
		}

		public static void Destroy(HorCupContext context)
		{
			context.Database.EnsureDeleted();

			context.Dispose();
		}
	}
}