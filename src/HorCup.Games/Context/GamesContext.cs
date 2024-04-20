using HorCup.Games.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Games.Context
{
	public class GamesContext : DbContext, IGamesContext
	{
		public GamesContext(DbContextOptions<GamesContext> options) : base(options)
		{
		}

		public DbSet<Game> Games { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
		}
	}
}