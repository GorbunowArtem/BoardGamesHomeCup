using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Infrastructure.Exceptions;
using HorCup.Players.Context;
using HorCup.Players.Models;
using HorCup.Players.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Players.Queries.GetById
{
	public class GetPlayerByIdQueryHandler: IRequestHandler<GetPlayerByIdQuery, PlayerDetailsViewModel>
	{
		private readonly IPlayersContext _context;
		private readonly ILogger<GetPlayerByIdQueryHandler> _logger;
		private readonly IMapper _mapper;
		
		public GetPlayerByIdQueryHandler(IPlayersContext context, ILogger<GetPlayerByIdQueryHandler> logger,
			IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<PlayerDetailsViewModel> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
		{
			var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

			if (player == null)
			{
				_logger.LogError($"Player with id {request.Id} was not found");
				throw new NotFoundException(nameof(Player), request.Id);
			}

			return _mapper.Map<PlayerDetailsViewModel>(player);
		}
	}
}