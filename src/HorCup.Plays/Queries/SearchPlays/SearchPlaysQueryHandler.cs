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
using MongoDB.Bson;
using MongoDB.Driver;

namespace HorCup.Plays.Queries.SearchPlays;

public class
	SearchPlaysQueryHandler : IRequestHandler<SearchPlaysQuery, (IEnumerable<PlayViewModel> items, long total)>
{
	private readonly IPlaysContext _context;
	private readonly ILogger<SearchPlaysQueryHandler> _logger;
	private readonly IMapper _mapper;

	public SearchPlaysQueryHandler(
		IPlaysContext context,
		ILogger<SearchPlaysQueryHandler> logger,
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

		ApplyPlayersFilter(request, ref filter);

		ApplyDatesFilter(request, ref filter);

		ApplySearchTextFilter(request, ref filter);
										  
		var plays = await _context.Plays.Find(filter)
			.Skip(request.Skip)
			.Limit(request.Take)
			.Sort(Builders<Play>.Sort.Descending(b => b.PlayedDate))
			.ToListAsync(cancellationToken);

		var count = await _context.Plays.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

		return (_mapper.Map<IEnumerable<PlayViewModel>>(plays), count);
	}

	private static void ApplySearchTextFilter(SearchPlaysQuery request, ref FilterDefinition<Play> filter)
	{
		if (!string.IsNullOrEmpty(request.SearchText))
		{
			filter &= Builders<Play>.Filter.Or(
				Builders<Play>.Filter.Regex(p => p.Game.Title, new BsonRegularExpression(request.SearchText))
				,Builders<Play>.Filter.ElemMatch(p => p.PlayerScores,
					pl => pl.Player.Name.Contains(request.SearchText)));
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
			filter &= Builders<Play>.Filter.ElemMatch(p => p.PlayerScores,
				ps => request.PlayersIds.Contains(ps.Player.Id));
		}
	}

	private static void ApplyDatesFilter(SearchPlaysQuery request, ref FilterDefinition<Play> filter)
	{
		if (request.DateFrom.HasValue && request.DateTo.HasValue)
		{
			filter &= Builders<Play>.Filter.And(
				Builders<Play>.Filter.Gte(p => p.PlayedDate, request.DateFrom),
				Builders<Play>.Filter.Lte(p => p.PlayedDate, request.DateTo));
		}
		else
		{
			if (request.DateFrom.HasValue)
			{
				filter &= Builders<Play>.Filter.Gte(p => p.PlayedDate, request.DateFrom);
			}
			else if (request.DateTo.HasValue)
			{
				filter &= Builders<Play>.Filter.Lte(p => p.PlayedDate, request.DateTo);
			}
		}
	}
}