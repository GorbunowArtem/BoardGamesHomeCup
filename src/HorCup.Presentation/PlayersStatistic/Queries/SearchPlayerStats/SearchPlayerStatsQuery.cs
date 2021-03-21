using System;
using System.Collections.Generic;
using HorCup.Presentation.Common.Queries;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats
{
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
}