using System;
using HorCup.Players.Context;
using HorCup.Players.Tests.Factory;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Tests
{
	public static class PlayersContextFactory
	{
		public static PlayersContext Create()
		{
			var options = new DbContextOptionsBuilder<PlayersContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new PlayersContext(options);
			context.Database.EnsureCreated();

			context.Players.AddRange(new PlayersFactory().Players);

			context.SaveChanges();

			return context;
		}

		public static void Destroy(PlayersContext context)
		{
			context.Database.EnsureDeleted();

			context.Dispose();
		}
	}
}                                              