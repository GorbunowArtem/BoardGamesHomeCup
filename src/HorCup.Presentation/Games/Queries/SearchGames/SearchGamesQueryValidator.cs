using FluentValidation;
using HorCup.Presentation.Common.Queries;

namespace HorCup.Presentation.Games.Queries.SearchGames
{
	public class SearchGamesQueryValidator: AbstractValidator<SearchGamesQuery>
	{
		public SearchGamesQueryValidator()
		{
			var constraints = new GamesConstraints();
			
			Include(new SearchQueryBaseValidator());

			RuleFor(s => s.MaxPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MaxPlayers);
			
			RuleFor(s => s.MinPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MinPlayers);

		}
	}
}