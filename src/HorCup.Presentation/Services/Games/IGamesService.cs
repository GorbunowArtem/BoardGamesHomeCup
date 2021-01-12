using System;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Games;

namespace HorCup.Presentation.Services.Games
{
	public interface IGamesService
	{
		Task<bool> IsTitleUniqueAsync(string title, Guid? id);
		
		Task<Game> TryGetGameAsync(Guid id, CancellationToken cancellationToken);

		Task ThrowIfNotExists(Guid id, CancellationToken cancellationToken);
	}
}