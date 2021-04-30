using System.Diagnostics.CodeAnalysis;
using HorCup.Presentation.Games;

namespace HorCup.Presentation.ViewModels
{
	[ExcludeFromCodeCoverage]
	public record ConstraintsViewModel
	{
		public GamesConstraints GamesConstraints => new GamesConstraints();

		// public PlayerConstraints PlayerConstraints => new PlayerConstraints();
	}
}