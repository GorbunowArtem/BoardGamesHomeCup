using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Queries.SearchPlayers
{
	public class SearchPlayersQueryHandler: IRequestHandler<SearchPlayersQuery, (IEnumerable<PlayerViewModel> items, int total)>
	{
		private readonly HorCupContext _context;
		private readonly ILogger<SearchPlayersQueryHandler> _logger;
		private readonly IMapper _mapper;

		public SearchPlayersQueryHandler(HorCupContext context, ILogger<SearchPlayersQueryHandler> logger,
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
			if (request.ExceptIds != null && request.ExceptIds.Any())
			{
				query = query.Where(p => !request.ExceptIds.Contains(p.Id));
			}

			return query;
		}

		private static IQueryable<Player> ApplySearchTextFilter(SearchPlayersQuery request, IQueryable<Player> query)
		{
			if (!string.IsNullOrEmpty(request.SearchText))
			{
				var searchTextUpper = request.SearchText.Trim().ToUpperInvariant();
				query = query.Where(p => p.FirstName.ToUpper().Contains(searchTextUpper)
				                         || p.LastName.ToUpper().Contains(searchTextUpper)
				                         || p.Nickname.ToUpper().Contains(searchTextUpper));
			}

			return query;
		}
	}
}