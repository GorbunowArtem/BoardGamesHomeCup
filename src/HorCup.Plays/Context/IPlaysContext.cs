using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Plays.Context
{
	public interface IPlaysContext
	{
		DbSet<Play> Plays { get; set; }

		DbSet<PlayScore> PlayScores { get; set; }
		
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}