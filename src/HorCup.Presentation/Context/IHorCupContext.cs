using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Games;
using HorCup.Presentation.GamesStatistic;
using HorCup.Presentation.PlayersStatistic;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HorCup.Presentation.Context
{
	public interface IHorCupContext
	{
		DbSet<Game> Games { get; set; }

		DbSet<Play> Plays { get; set; }

		DbSet<PlayScore> PlayScores { get; set; }

		DbSet<GameStatistic> GamesStatistics { get; set; }
		
		DbSet<PlayerStatistic> PlayersStatistics { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		
		DatabaseFacade Database { get; }
	}
}