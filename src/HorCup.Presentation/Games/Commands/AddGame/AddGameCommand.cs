using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Commands.AddGame
{
	public class AddGameCommand: IRequest<GameViewModel>
	{
		public string Title { get; set; }

		public int MaxPlayers { get; set; }
	
		public int MinPlayers { get; set; }
	}
}