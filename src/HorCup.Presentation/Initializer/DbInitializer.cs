using System;
using System.Collections.Generic;
using System.Linq;
using HorCup.Presentation.Context;
using HorCup.Presentation.Players;

namespace HorCup.Presentation.Initializer
{
	public class DbInitializer
	{
		public static void Initialize(HorCupContext context)
		{
			context.Database.EnsureCreated();

			if (context.Players.Any())
			{
				return;
			}

			context.Players.AddRange(GetPlayers());
			context.SaveChanges();
		}

		private static IEnumerable<Player> GetPlayers()
		{
			return Enumerable.Range(1,15).Select(i => new Player
			{
				Added = DateTime.Now,
				Nickname = $"player{i}",
				BirthDate = new DateTime(1990, 2, i),
				FirstName = $"Player{i}",
				LastName = $"Player{i}"
			});
		}
	}
}