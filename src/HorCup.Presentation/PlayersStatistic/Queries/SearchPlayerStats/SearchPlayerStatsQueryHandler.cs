using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats
{
	public class SearchPlayerStatsQueryHandler : IRequestHandler<SearchPlayerStatsQuery, (
		IEnumerable<PlayerStatisticViewModel> items, int total)>
	{
		private readonly IHorCupContext _context;
		private readonly IMapper _mapper;

		public SearchPlayerStatsQueryHandler(IHorCupContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<(IEnumerable<PlayerStatisticViewModel> items, int total)> Handle(
			SearchPlayerStatsQuery request,
			CancellationToken cancellationToken)
		{
			var query = _context.PlayersStatistics.AsQueryable();

			if (request.GameId.HasValue)
			{
				query = query.Where(p => p.GameId == request.GameId);
			}

			query = request.SortBy switch
			{
				PlayerStatsSortBy.TotalPlayed => query.OrderByDescending(p => p.PlayedTotal),
				PlayerStatsSortBy.TotalWins => query.OrderByDescending(p => p.Wins),
				PlayerStatsSortBy.AverageScore => query.OrderByDescending(p => p.AverageScore),
				_ => query.OrderByDescending(p => p.PlayedTotal)
			};

			var stats = await query.Include(p => p.Players)
				.Skip(request.Skip)
				.Take(request.Take)
				.ToListAsync(cancellationToken);

			var total = await query.CountAsync(cancellationToken);

			return (_mapper.Map<IEnumerable<PlayerStatisticViewModel>>(stats), total);
		}
	}
}