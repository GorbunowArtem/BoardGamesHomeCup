using HorCup.Presentation.Games;
using HorCup.Presentation.Players;

namespace HorCup.Presentation.ViewModels
{
	public record ConstraintsViewModel
	{
		public GamesConstraints GamesConstraints => new GamesConstraints();

		public PlayerConstraints PlayerConstraints => new PlayerConstraints();
	}
}