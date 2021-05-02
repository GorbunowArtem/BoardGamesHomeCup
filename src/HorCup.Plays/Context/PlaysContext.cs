using HorCup.Plays.PlayScores;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Plays.Context
{
	public class PlaysContext : DbContext, IPlaysContext
	{
		public PlaysContext(DbContextOptions<PlaysContext> options) : base(options)
		{
		}

		public DbSet<Play> Plays { get; set; }

		public DbSet<PlayScore> PlayScores { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayConfiguration).Assembly);
		}
	}
}