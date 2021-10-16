using System.Collections.Generic;
using Akkatecture.Specifications;

namespace HorCup.Games.Models
{
	public class ValidPlayersAmountSpecification: Specification<GameState>
	{
		protected override IEnumerable<string> IsNotSatisfiedBecause(GameState state)
		{
			var gamesConstraint = new GamesConstraints();
			
			if (state.MaxPlayers < 0 || state.MaxPlayers > gamesConstraint.MaxPlayers)
			{
				yield return $"Maximum players is less than zero or more than {gamesConstraint.MaxPlayers.ToString()}";
			}
			
			if (state.MinPlayers < 0 || state.MinPlayers > gamesConstraint.MinPlayers)
			{
				yield return $"Minimum players is less than zero or more than {gamesConstraint.MaxPlayers.ToString()}";
			}
		}
	}
}