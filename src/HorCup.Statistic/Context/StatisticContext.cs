using HorCup.Statistic.GamesStatistic;
using HorCup.Statistic.PlayersStatistic;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Statistic.Context
{
	public class StatisticContext : DbContext, IStatisticContext
	{
		public StatisticContext(DbContextOptions<StatisticContext> options) : base(options)
		{
		}


		public DbSet<GameStatistic> GamesStatistics { get; set; }

		public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StatisticContext).Assembly);
		}
	}
}