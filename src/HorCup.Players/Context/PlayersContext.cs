using System.Threading;
using System.Threading.Tasks;
using HorCup.Players.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Context
{
	public class PlayersContext: IPlayersContext
	{
		public DbSet<Player> Players { get; set; }
		
		public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}