using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Players;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public interface IHorCupContext
	{
		DbSet<Player> Players { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}