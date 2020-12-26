using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public class SearchPlaysQueryHandler: IRequestHandler<SearchPlaysQuery, (IEnumerable<PlayViewModel> items, int total)>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<SearchPlaysQueryHandler> _logger;

		public SearchPlaysQueryHandler(IHorCupContext context, ILogger<SearchPlaysQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<(IEnumerable<PlayViewModel> items, int total)> Handle(SearchPlaysQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Plays.AsQueryable();

			query = ApplyGamesFilter(request, query);

			query = ApplyPlayersFilter(request, query);

			query = ApplyDatesFilter(request, query);
			
			var plays = await query
				.Include(p => p.Game)
				.Include(p => p.PlayerScores)
				.ThenInclude(p => p.Player)
				.Select(p => new PlayViewModel
				{
					Id = p.Id,
					Notes = p.Notes,
					GameTitle = p.Game.Title,
					GameId = p.GameId,
					PlayedDate = p.PlayedDate,
					PlayerScores = p.PlayerScores.Select(ps => new PlayScoreViewModel(
						new IdName(
							ps.PlayerId,
							$"{ps.Player.FirstName} {ps.Player.LastName}"),
							ps.Score,
						 ps.IsWinner
					))  
				})
				.OrderByDescending(pl => pl.PlayedDate)
				.Skip(request.Skip)
				.Take(request.Take)
				.ToListAsync(cancellationToken);

			var total = await query.CountAsync(cancellationToken);
			
			return (plays, total);
		}

		private static IQueryable<Play> ApplyGamesFilter(SearchPlaysQuery request, IQueryable<Play> query)
		{
			if (request.GamesIds.Any())
			{
				query = query.Where(p => request.GamesIds.Contains(p.GameId));
			}

			return query;
		}

		private static IQueryable<Play> ApplyPlayersFilter(SearchPlaysQuery request, IQueryable<Play> query)
		{
			if (request.PlayersIds.Any())
			{
				query = query.Where(p => p.PlayerScores.Any(ps => request.PlayersIds.Contains(ps.PlayerId)));
			}

			return query;
		}

		private static IQueryable<Play> ApplyDatesFilter(SearchPlaysQuery request, IQueryable<Play> query)
		{
			if (request.DateFrom.HasValue && request.DateTo.HasValue)
			{
				query = query.Where(p => p.PlayedDate >= request.DateFrom && p.PlayedDate <= request.DateTo);
			}
			else
			{
				if (request.DateFrom.HasValue)
				{
					query = query.Where(p => p.PlayedDate >= request.DateFrom);
				}
				else if (request.DateTo.HasValue)
				{
					query = query.Where(p => p.PlayedDate <= request.DateTo);
				}
			}

			return query;
		}
	}
}