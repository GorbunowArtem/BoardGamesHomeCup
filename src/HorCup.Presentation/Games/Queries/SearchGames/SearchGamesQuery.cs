using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Queries.SearchGames
{
	public class SearchGamesQuery : IRequest<(IEnumerable<GameViewModel> items, int total)>
	{
		public int Take { get; set; } = 10;

		public int Skip { get; set; }

		public string SearchText { get; set; }

		public int? MinPlayers { get; set; }

		public int? MaxPlayers { get; set; }
	}
}