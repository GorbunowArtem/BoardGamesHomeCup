using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public interface IHorCupContext
	{
		DbSet<Player> Players { get; set; }

		DbSet<Game> Games { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}