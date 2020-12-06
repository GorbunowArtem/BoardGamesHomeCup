using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.Queries.GetById
{
	public class GetGameByIdQueryHandler: IRequestHandler<GetGameByIdQuery, GameDetailsViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GetGameByIdQueryHandler> _logger;
		private readonly IMapper _mapper;

		public GetGameByIdQueryHandler(IHorCupContext context, ILogger<GetGameByIdQueryHandler> logger,
			IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<GameDetailsViewModel> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
		{
			var game = await _context.Games.Where(g => g.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

			if (game == null)
			{
				_logger.LogError($"Game with id {request.Id} was not found");
				throw new NotFoundException(nameof(Game), request.Id);
			}

			return _mapper.Map<GameDetailsViewModel>(game);
		}
	}
}