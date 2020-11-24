using HorCup.Presentation.Players;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public class HorCupContext : DbContext, IHorCupContext
	{
		public HorCupContext(DbContextOptions<HorCupContext> options) : base(options)
		{
		}

		public DbSet<Player> Players { get; set; }
	}
}