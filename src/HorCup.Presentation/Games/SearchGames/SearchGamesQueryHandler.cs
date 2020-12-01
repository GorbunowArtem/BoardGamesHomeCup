using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Games.SearchGames
{
	public class SearchGamesQueryHandler : IRequestHandler<SearchGamesQuery, GameViewModel>
	{
		private readonly IHorCupContext _context;
		private readonly IMapper _mapper;
		private readonly ILogger<SearchGamesQueryHandler> _logger;

		public SearchGamesQueryHandler(
			IHorCupContext context,
			IMapper mapper,
			ILogger<SearchGamesQueryHandler> logger)
		{
			_context = context;
			_mapper = mapper;
			_logger = logger;
		}

		public Task<GameViewModel> Handle(SearchGamesQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}