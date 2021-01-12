using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Services.Games;
using MediatR;

namespace HorCup.Presentation.Games.Queries.IsTitleUnique
{
	public class IsTitleUniqueQueryHandler:IRequestHandler<IsTitleUniqueQuery, bool>
	{
		private readonly IGamesService _gamesService;

		public IsTitleUniqueQueryHandler(IGamesService gamesService)
		{
			_gamesService = gamesService;
		}

		public async Task<bool> Handle(IsTitleUniqueQuery request, CancellationToken cancellationToken)
		{
			var (title, guid) = request;
			
			return await _gamesService.IsTitleUniqueAsync(title, guid, cancellationToken);
		}
	}
}