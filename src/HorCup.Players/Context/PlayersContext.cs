using HorCup.Players.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Context;

public class PlayersContext : DbContext, IPlayersContext
{
	public PlayersContext(DbContextOptions<PlayersContext> options) : base(options)
	{
	}

	public DbSet<Player> Players { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayersContext).Assembly);
	}
}