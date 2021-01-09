using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Queries.GetDetails
{
	public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GetGameDetailsQueryHandler> _logger;
		private readonly IGamesService _gamesService;
		private readonly IMapper _mapper;

		public GetGameDetailsQueryHandler(
			IHorCupContext context,
			ILogger<GetGameDetailsQueryHandler> logger,
			IGamesService gamesService,
			IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_gamesService = gamesService;
			_mapper = mapper;
		}

		public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
		{
			await _gamesService.ThrowIfNotExists(request.Id, cancellationToken);
			
			var game = await _context.Games.Where(g => g.Id == request.Id)
				.Include(gs => gs.GameStatistic)
				.SingleAsync(cancellationToken);

			return _mapper.Map<GameDetailsViewModel>(game);
		}
	}
}