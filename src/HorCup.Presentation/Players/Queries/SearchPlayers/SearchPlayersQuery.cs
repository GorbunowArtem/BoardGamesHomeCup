using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Players.Queries.SearchPlayers
{
	public class SearchPlayersQuery: IRequest<(IEnumerable<PlayerViewModel> items, int total)>
	{
		public int Take { get; set; } = 10;

		public int Skip { get; set; }

		public string SearchText { get; set; } = string.Empty;
	}
}