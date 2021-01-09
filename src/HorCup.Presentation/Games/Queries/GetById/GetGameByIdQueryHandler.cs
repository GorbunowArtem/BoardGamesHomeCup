using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Services.Games;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Queries.GetById
{
	public class GetGameByIdQueryHandler: IRequestHandler<GetGameByIdQuery, GameDetailsViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GetGameByIdQueryHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IGamesService _gamesService;

		public GetGameByIdQueryHandler(IHorCupContext context, ILogger<GetGameByIdQueryHandler> logger,
			IMapper mapper,
			IGamesService gamesService)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
			_gamesService = gamesService;
		}

		public async Task<GameDetailsViewModel> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
		{
			var game = await _gamesService.TryGetGameAsync(request.Id, cancellationToken);
			
			return _mapper.Map<GameDetailsViewModel>(game);
		}
	}
}