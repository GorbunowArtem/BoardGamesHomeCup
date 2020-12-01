using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.SearchGames
{
	public class SearchGamesQuery : IRequest<GameViewModel>
	{
		public int Take { get; set; }

		public int Skip { get; set; }

		public string SearchText { get; set; }

		public int? MinPlayers { get; set; }

		public int? MaxPlayers { get; set; }
	}
}