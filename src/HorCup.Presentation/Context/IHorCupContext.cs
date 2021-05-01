using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.GamesStatistic;
using HorCup.Presentation.PlayersStatistic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HorCup.Presentation.Context
{
	public interface IHorCupContext
	{
		DbSet<GameStatistic> GamesStatistics { get; set; }
		
		DbSet<PlayerStatistic> PlayersStatistics { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		
		DatabaseFacade Database { get; }
	}
}