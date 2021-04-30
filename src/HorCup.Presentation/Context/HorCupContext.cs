using HorCup.Presentation.GamesStatistic;
using HorCup.Presentation.PlayersStatistic;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public class HorCupContext : DbContext, IHorCupContext
	{
		public HorCupContext(DbContextOptions<HorCupContext> options) : base(options)
		{
		}

		public DbSet<Play> Plays { get; set; }
		
		public DbSet<PlayScore> PlayScores { get; set; }
		
		public DbSet<GameStatistic> GamesStatistics { get; set; }
		
		public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HorCupContext).Assembly);
		}
	}
}