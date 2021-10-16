using System.Collections.Generic;
using Akkatecture.Specifications;

namespace HorCup.Games.Models
{
	public class ValidTitleSpecification: Specification<GameState>
	{
		protected override IEnumerable<string> IsNotSatisfiedBecause(GameState state)
		{
			if (string.IsNullOrWhiteSpace(state.Title))
			{
				yield return "Game title cannot be empty";
			}

			var gameConstraints = new GamesConstraints();
			
			if (state.Title.Length > gameConstraints.TitleMaxLength)
			{
				yield return $"Game title cannot be bigger than {gameConstraints.TitleMaxLength.ToString()}";
			}
		}
	}
}