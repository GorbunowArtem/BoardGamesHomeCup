using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Plays.Context;
using HorCup.Plays.Models;
using HorCup.Plays.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace HorCup.Plays.Queries.SearchPlays
{
	public class SearchPlaysQueryHandler : IRequestHandler<SearchPlaysQuery, (IEnumerable<PlayViewModel> items, long total)>
	{
		private readonly IPlaysContext _context;
		private readonly ILogger<SearchPlaysQueryHandler> _logger;
		private readonly IMapper _mapper;
		
		public SearchPlaysQueryHandler(IPlaysContext context, ILogger<SearchPlaysQueryHandler> logger,
			IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<(IEnumerable<PlayViewModel> items, long total)> Handle(
			SearchPlaysQuery request,
			CancellationToken cancellationToken)
		{
			var filter = Builders<Play>.Filter.Empty;
			
			ApplyGamesFilter(request, ref filter);
			
			// ApplyPlayersFilter(request, ref filter);
			
			// ApplyDatesFilter(request, filter);
			
			// ApplySearchTextFilter(request, filter);


			var plays = await _context.Plays.Find(filter)
				.Skip(request.Skip)
				.Limit(request.Take)
				.Sort(Builders<Play>.Sort.Descending(b => b.PlayedDate))
				.ToListAsync(cancellationToken);

			var count = await _context.Plays.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
			
			return (_mapper.Map<IEnumerable<PlayViewModel>>(plays), count);
		}

		private static void ApplySearchTextFilter(SearchPlaysQuery request, IQueryable<Play> filter)
		{
			if (!string.IsNullOrEmpty(request.SearchText))
			{
				// filter = filter.Where(p => EF.Functions.Like(p.Game.Title, $"%{request.SearchText}%")
				//                          || p.PlayerScores.Any(ps =>
				// 	                         EF.Functions.Like(ps.Player.Nickname, $"%{request.SearchText}")));
			}
		}

		private static void ApplyGamesFilter(SearchPlaysQuery request, ref FilterDefinition<Play> filter)
		{
			if (request.GamesIds != null && request.GamesIds.Any())
			{
				filter &= Builders<Play>.Filter.In(p => p.Game.Id, request.GamesIds);
			}
		}

		private static void ApplyPlayersFilter(SearchPlaysQuery request, ref FilterDefinition<Play> filter)
		{
			if (request.PlayersIds != null && request.PlayersIds.Any())
			{
				filter &= Builders<Play>.Filter.AnyIn(p => p.PlayerScores.Select(pl => pl.Player.Id).ToArray(), request.PlayersIds);
			}
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