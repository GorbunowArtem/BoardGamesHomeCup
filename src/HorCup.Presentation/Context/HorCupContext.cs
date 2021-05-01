using HorCup.Presentation.GamesStatistic;
using HorCup.Presentation.PlayersStatistic;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public class HorCupContext : DbContext, IHorCupContext
	{
		public HorCupContext(DbContextOptions<HorCupContext> options) : base(options)
		{
		}


		public DbSet<GameStatistic> GamesStatistics { get; set; }

		public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HorCupContext).Assembly);
		}
	}
}