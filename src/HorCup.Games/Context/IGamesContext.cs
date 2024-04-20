using System.Threading;
using System.Threading.Tasks;
using HorCup.Games.Models;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Games.Context
{
	public interface IGamesContext
	{
		DbSet<Game> Games { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}