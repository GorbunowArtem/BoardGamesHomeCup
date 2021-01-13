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

namespace HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats
{
	public class SearchPlayerStatsQueryHandler : IRequestHandler<SearchPlayerStatsQuery, (
		IEnumerable<PlayerStatisticViewModel> items, int total)>
	{
		private readonly IHorCupContext _context;
		private readonly IMapper _mapper;
		private readonly ILogger<SearchPlayerStatsQueryHandler> _logger;

		public SearchPlayerStatsQueryHandler(IHorCupContext context, IMapper mapper,
			ILogger<SearchPlayerStatsQueryHandler> logger)
		{
			_context = context;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<(IEnumerable<PlayerStatisticViewModel> items, int total)> Handle(
			SearchPlayerStatsQuery request,
			CancellationToken cancellationToken)
		{
			var query = _context.PlayersStatistics.AsQueryable();

			query = ApplyGameIdFilter(request, query);

			query = ApplyPlayerIdFilter(request, query);

			query = ApplySortOrder(request, query);

			var stats = await query.Include(p => p.Player)
				.Include(g => g.Game)
				.Skip(request.Skip)
				.Take(request.Take)
				.ToListAsync(cancellationToken);

			var total = await query.CountAsync(cancellationToken);

			return (_mapper.Map<IEnumerable<PlayerStatisticViewModel>>(stats), total);
		}

		private IQueryable<PlayerStatistic> ApplySortOrder(SearchPlayerStatsQuery request, IQueryable<PlayerStatistic> query)
		{
			_logger.LogInformation($"Applying sort order {request.SortBy.ToString()} filter.");
			query = request.SortBy switch
			{
				PlayerStatsSortBy.TotalPlayed => query.OrderByDescending(p => p.PlayedTotal),
				PlayerStatsSortBy.TotalWins => query.OrderByDescending(p => p.Wins),
				PlayerStatsSortBy.AverageScore => query.OrderByDescending(p => p.AverageScore),
				_ => query.OrderByDescending(p => p.PlayedTotal)
			};
			return query;
		}

		private IQueryable<PlayerStatistic> ApplyPlayerIdFilter(SearchPlayerStatsQuery request, IQueryable<PlayerStatistic> query)
		{
			if (request.PlayerId.HasValue)
			{
				_logger.LogInformation($"Applying playerId {request.PlayerId.ToString()} filter.");
				query = query.Where(p => p.PlayerId == request.PlayerId);
			}

			return query;
		}

		private IQueryable<PlayerStatistic> ApplyGameIdFilter(SearchPlayerStatsQuery request, IQueryable<PlayerStatistic> query)
		{
			if (request.GameId.HasValue)
			{
				_logger.LogInformation($"Applying gameId {request.GameId.ToString()} filter.");
				query = query.Where(p => p.GameId == request.GameId);
			}

			return query;
		}
	}
}