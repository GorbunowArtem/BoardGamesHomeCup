using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Players.Context;
using HorCup.Players.Models;
using HorCup.Players.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Players.Players.Queries.SearchPlayers
{
	public class SearchPlayersQueryHandler: IRequestHandler<SearchPlayersQuery, (IEnumerable<PlayerViewModel> items, int total)>
	{
		private readonly IPlayersContext _context;
		private readonly ILogger<SearchPlayersQueryHandler> _logger;
		private readonly IMapper _mapper;

		public SearchPlayersQueryHandler(IPlayersContext context, ILogger<SearchPlayersQueryHandler> logger,
			IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<(IEnumerable<PlayerViewModel> items, int total)> Handle(SearchPlayersQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Players.Where(p => true);

			query = ApplySearchTextFilter(request, query);

			query = ApplyExceptIdsFilter(request, query);
			
			var players = await query
				.OrderByDescending(p => p.Added)
				.Skip(request.Skip)
				.Take(request.Take)
				.ToListAsync(cancellationToken);

			
			return (_mapper.Map<IEnumerable<PlayerViewModel>>(players), query.Count());
		}

		private static IQueryable<Player> ApplyExceptIdsFilter(SearchPlayersQuery request, IQueryable<Player> query)
		{
			if (request.ExceptIds.Any())
			{
				query = query.Where(p => !request.ExceptIds.Contains(p.Id));
			}

			return query;
		}

		private static IQueryable<Player> ApplySearchTextFilter(SearchPlayersQuery request, IQueryable<Player> query)
		{
			if (!string.IsNullOrEmpty(request.SearchText))
			{
				query = query.Where(p => EF.Functions.Like(p.Nickname, $"%{request.SearchText.Trim()}%"));
			}

			return query;
		}
	}
}