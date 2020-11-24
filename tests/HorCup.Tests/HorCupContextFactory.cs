using System;
using HorCup.Presentation.Context;
using HorCup.Tests.Players.Factory;
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