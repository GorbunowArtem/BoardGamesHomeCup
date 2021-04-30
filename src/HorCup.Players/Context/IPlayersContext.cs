using System.Threading;
using System.Threading.Tasks;
using HorCup.Players.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Players.Context
{
	public interface IPlayersContext
	{
		DbSet<Player> Players { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}