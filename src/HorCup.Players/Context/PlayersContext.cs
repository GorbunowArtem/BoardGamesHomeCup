using HorCup.Players.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Context
{
	public class PlayersContext
	{
		public DbSet<Player> Players { get; set; }
	}
}