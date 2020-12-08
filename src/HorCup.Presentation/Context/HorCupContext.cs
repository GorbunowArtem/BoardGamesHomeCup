using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public class HorCupContext : DbContext, IHorCupContext
	{
		public HorCupContext(DbContextOptions<HorCupContext> options) : base(options)
		{
		}

		public DbSet<Player> Players { get; set; }

		public DbSet<Game> Games { get; set; }

		public DbSet<Play> Plays { get; set; }
	}
}