using System;
using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats
{
	public class SearchPlayerStatsQuery : IRequest<(IEnumerable<PlayerStatisticViewModel> items, int total)>
	{
		public Guid? GameId { get; set; }

		public PlayerStatsSortBy SortBy { get; set; }

		public int Take { get; set; }

		public int Skip { get; set; }
	}
}