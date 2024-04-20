using System.Threading;
using System.Threading.Tasks;
using HorCup.Statistic.GamesStatistic;
using HorCup.Statistic.PlayersStatistic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HorCup.Statistic.Context
{
	public interface IStatisticContext
	{
		DbSet<GameStatistic> GamesStatistics { get; set; }
		
		DbSet<PlayerStatistic> PlayersStatistics { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		
		DatabaseFacade Database { get; }
	}
}