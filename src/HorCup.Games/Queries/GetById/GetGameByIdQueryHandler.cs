using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Games.Context;
using HorCup.Games.Services.Games;
using HorCup.Games.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Games.Queries.GetById;

public class GetGameByIdQueryHandler: IRequestHandler<GetGameByIdQuery, GameDetailsViewModel>
{
	private readonly IGamesContext _context;
	private readonly ILogger<GetGameByIdQueryHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IGamesService _gamesService;

	public GetGameByIdQueryHandler(IGamesContext context, ILogger<GetGameByIdQueryHandler> logger,
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
		await _gamesService.TryGetGameAsync(request.Id, cancellationToken);
			
		var game = await _context.Games.Where(g => g.Id == request.Id)
			.SingleAsync(cancellationToken);

		return _mapper.Map<GameDetailsViewModel>(game);
	}
}