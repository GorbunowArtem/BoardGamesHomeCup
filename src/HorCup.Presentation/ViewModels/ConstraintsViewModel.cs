using HorCup.Presentation.Games;
using HorCup.Presentation.Players;

namespace HorCup.Presentation.ViewModels
{
	public class ConstraintsViewModel
	{
		public GamesConstraints GamesConstraints => new GamesConstraints();

		public PlayerConstraints PlayerConstraints => new PlayerConstraints();
	}
}