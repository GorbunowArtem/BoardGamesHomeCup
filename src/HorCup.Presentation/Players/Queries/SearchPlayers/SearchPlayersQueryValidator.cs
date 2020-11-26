using FluentValidation;

namespace HorCup.Presentation.Players.Queries.SearchPlayers
{
	public class SearchPlayersQueryValidator: AbstractValidator<SearchPlayersQuery>
	{
		public SearchPlayersQueryValidator()
		{
			RuleFor(sp => sp.Take)
				.GreaterThan(0)
				.LessThan(100);

			RuleFor(sp => sp.Skip)
				.GreaterThan(0);
		}
	}
}