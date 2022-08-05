using System;
using System.Collections.Generic;
using HorCup.Infrastructure.Queries;
using HorCup.Statistic.ViewModels;
using MediatR;

namespace HorCup.Statistic.PlayersStatistic.Queries.SearchPlayerStats;

public record SearchPlayerStatsQuery : SearchQueryBase, IRequest<(IEnumerable<PlayerStatisticViewModel> items, int total)>
{
	public Guid? GameId { get; set; }

	public PlayerStatsSortBy SortBy { get; set; }

	public Guid? PlayerId { get; set; }
		
	public SearchPlayerStatsQuery()
	{
		Take = 5;
	}
}