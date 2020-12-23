using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.Context
{
	public interface IHorCupContext
	{
		DbSet<Player> Players { get; set; }

		DbSet<Game> Games { get; set; }

		DbSet<Play> Plays { get; set; }

		DbSet<PlayScore> PlayScores { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}