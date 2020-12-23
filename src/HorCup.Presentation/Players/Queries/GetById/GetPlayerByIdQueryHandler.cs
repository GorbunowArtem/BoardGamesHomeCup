using System;
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

namespace HorCup.Presentation.Players.Queries.GetById
{
	public class GetPlayerByIdQueryHandler: IRequestHandler<GetPlayerByIdQuery, PlayerDetailsViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GetPlayerByIdQueryHandler> _logger;
		private readonly IMapper _mapper;
		
		public GetPlayerByIdQueryHandler(IHorCupContext context, ILogger<GetPlayerByIdQueryHandler> logger,
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